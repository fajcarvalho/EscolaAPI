using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using MediatR;

namespace EscolaAPI.Application.Commands.Cursos 
{
    public class CreateCursoCommand : IRequest<int>
    {
        public CreateCursoDto CursoDto { get; set; }

        public CreateCursoCommand(CreateCursoDto cursoDto) 
        {
            CursoDto = cursoDto;
        }
    }

    public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommand, int> 
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;

        public CreateCursoCommandHandler(
            ICursoRepository cursoRepository,
            IDepartamentoRepository departamentoRepository,
            IMapper mapper) 
        {
            _cursoRepository = cursoRepository;
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCursoCommand request, CancellationToken cancellationToken) 
        {
            // Verificar se o departamento existe
            var departamento = await _departamentoRepository.GetByIdAsync(request.CursoDto.DepartamentoId);
            if (departamento == null)
                throw new ApplicationException("O departamento informado não existe.");

            var curso = _mapper.Map<Curso>(request.CursoDto);

            await _cursoRepository.AddAsync(curso);
            await _cursoRepository.SaveChangesAsync();

            return curso.Id;
        }
    }
}
