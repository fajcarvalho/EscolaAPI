using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Departamentos
{
    public class UpdateDepartamentoCommand : IRequest<bool>
    {
        public UpdateDepartamentoDto DepartamentoDto { get; set; }
        
        public UpdateDepartamentoCommand(UpdateDepartamentoDto departamentoDto)
        {
            DepartamentoDto = departamentoDto;
        }
    }
    
    public class UpdateDepartamentoCommandHandler : IRequestHandler<UpdateDepartamentoCommand, bool>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        
        public UpdateDepartamentoCommandHandler(
            IDepartamentoRepository departamentoRepository, 
            IProfessorRepository professorRepository,
            IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _professorRepository = professorRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateDepartamentoCommand request, CancellationToken cancellationToken)
        {
            var departamento = await _departamentoRepository.GetByIdAsync(request.DepartamentoDto.Id);
            if (departamento == null) return false;
            
            // Verificar se o coordenador existe, se houver
            if (request.DepartamentoDto.CordenadorId.HasValue)
            {
                var coordenador = await _professorRepository.GetByIdAsync(request.DepartamentoDto.CordenadorId.Value);
                if (coordenador == null)
                    throw new ApplicationException("O professor indicado como coordenador não existe.");
            }
            
            _mapper.Map(request.DepartamentoDto, departamento);
            
            await _departamentoRepository.UpdateAsync(departamento);
            return await _departamentoRepository.SaveChangesAsync();
        }
    }
}