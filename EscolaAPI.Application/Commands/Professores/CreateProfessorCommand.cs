using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using MediatR;

namespace EscolaAPI.Application.Commands.Professores
{
    public class CreateProfessorCommand : IRequest<int> 
    {
        public CreateProfessorDto ProfessorDto { get; set; }

        public CreateProfessorCommand(CreateProfessorDto professorDto) 
        {
            ProfessorDto = professorDto;
        }
    }
    
    public class CreateProfessorCommandHandler : IRequestHandler<CreateProfessorCommand, int> 
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;

        public CreateProfessorCommandHandler(
            IProfessorRepository professorRepository,
            IDepartamentoRepository departamentoRepository,
            IMapper mapper) 
        {
            _professorRepository = professorRepository;
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProfessorCommand request, CancellationToken cancellationToken) 
        {
            // Verificar se o departamento existe
            var departamento = await _departamentoRepository.GetByIdAsync(request.ProfessorDto.DepartamentoId);
            if (departamento == null)
                throw new ApplicationException("O departamento informado não existe.");

            var professor = _mapper.Map<Professor>(request.ProfessorDto);

            await _professorRepository.AddAsync(professor);
            await _professorRepository.SaveChangesAsync();

            return professor.Id;
        }
    }
}