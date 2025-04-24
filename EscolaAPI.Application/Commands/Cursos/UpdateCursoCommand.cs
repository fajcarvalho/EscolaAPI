using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Cursos
{
    public class UpdateCursoCommand : IRequest<bool>
    {
        public UpdateCursoDto CursoDto { get; set; }
        
        public UpdateCursoCommand(UpdateCursoDto cursoDto)
        {
            CursoDto = cursoDto;
        }
    }
    
    public class UpdateCursoCommandHandler : IRequestHandler<UpdateCursoCommand, bool>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;
        
        public UpdateCursoCommandHandler(
            ICursoRepository cursoRepository,
            IDepartamentoRepository departamentoRepository,
            IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateCursoCommand request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.GetByIdAsync(request.CursoDto.Id);
            if (curso == null) return false;
            
            // Verificar se o departamento existe, se fornecido
            if (request.CursoDto.DepartamentoId.HasValue)
            {
                var departamento = await _departamentoRepository.GetByIdAsync(request.CursoDto.DepartamentoId.Value);
                if (departamento == null)
                    throw new ApplicationException("O departamento informado não existe.");
            }
            
            _mapper.Map(request.CursoDto, curso);
            
            await _cursoRepository.UpdateAsync(curso);
            return await _cursoRepository.SaveChangesAsync();
        }
    }
}