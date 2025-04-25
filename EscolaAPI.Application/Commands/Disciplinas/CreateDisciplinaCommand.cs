using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using MediatR;

namespace EscolaAPI.Application.Commands.Disciplinas
{
    public class CreateDisciplinaCommand : IRequest<int>
    {
        public CreateDisciplinaDto DisciplinaDto { get; set; }
        
        public CreateDisciplinaCommand(CreateDisciplinaDto disciplinaDto)
        {
            DisciplinaDto = disciplinaDto;
        }
    }

    public class CreateDisciplinaCommandHandler : IRequestHandler<CreateDisciplinaCommand, int>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        
        public CreateDisciplinaCommandHandler(
            IDisciplinaRepository disciplinaRepository,
            ICursoRepository cursoRepository,
            IProfessorRepository professorRepository,
            IMapper mapper)
        {
            _disciplinaRepository = disciplinaRepository;
            _cursoRepository = cursoRepository;
            _professorRepository = professorRepository;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateDisciplinaCommand request, CancellationToken cancellationToken)
        {
            // Verificar se o curso existe
            var curso = await _cursoRepository.GetByIdAsync(request.DisciplinaDto.CursoId);
            if (curso == null)
                throw new ApplicationException("O curso informado não existe.");
                
            // Verificar se o código já está em uso
            var codigoExistente = await _disciplinaRepository.ExistsDisciplinaByCodigoAsync(request.DisciplinaDto.Codigo);
            if (codigoExistente)
                throw new ApplicationException($"O código {request.DisciplinaDto.Codigo} já está em uso por outra disciplina.");
                
            // Mapear e salvar a disciplina
            var disciplina = _mapper.Map<Disciplina>(request.DisciplinaDto);
            
            await _disciplinaRepository.AddAsync(disciplina);
            await _disciplinaRepository.SaveChangesAsync();
            
            // Adicionar pré-requisitos
            foreach (var preRequisitoId in request.DisciplinaDto.PreRequisitosIds)
            {
                // Verificar se o pré-requisito existe
                var preRequisito = await _disciplinaRepository.GetByIdAsync(preRequisitoId);
                if (preRequisito == null)
                    throw new ApplicationException($"A disciplina pré-requisito com ID {preRequisitoId} não existe.");
                    
                // Evitar auto-referência
                if (preRequisitoId == disciplina.Id)
                    throw new ApplicationException("Uma disciplina não pode ser pré-requisito dela mesma.");
                    
                await _disciplinaRepository.AddPreRequisitoAsync(disciplina.Id, preRequisitoId);
            }
            
            // Adicionar professores
            foreach (var professor in request.DisciplinaDto.Professores)
            {
                // Verificar se o professor existe
                var professorExistente = await _professorRepository.GetByIdAsync(professor.ProfessorId);
                if (professorExistente == null)
                    throw new ApplicationException($"O professor com ID {professor.ProfessorId} não existe.");
                    
                await _disciplinaRepository.AddProfessorAsync(disciplina.Id, professor.ProfessorId, professor.EhResponsavel);
            }
            
            return disciplina.Id;
        }
    }
}