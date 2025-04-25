using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using MediatR;

namespace EscolaAPI.Application.Commands.Disciplinas 
{
    public class UpdateDisciplinaCommand : IRequest<bool>
    {
        public UpdateDisciplinaDto DisciplinaDto { get; set; }
        
        public UpdateDisciplinaCommand(UpdateDisciplinaDto disciplinaDto)
        {
            DisciplinaDto = disciplinaDto;
        }
    }

    public class UpdateDisciplinaCommandHandler : IRequestHandler<UpdateDisciplinaCommand, bool>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;
        
        public UpdateDisciplinaCommandHandler(
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
        
        public async Task<bool> Handle(UpdateDisciplinaCommand request, CancellationToken cancellationToken)
        {
            // Buscar a disciplina com todos os detalhes
            var disciplina = await _disciplinaRepository.GetDisciplinaWithAllDetailsAsync(request.DisciplinaDto.Id);
            if (disciplina == null) return false;
            
            // Verificar o curso, se informado
            if (request.DisciplinaDto.CursoId.HasValue)
            {
                var curso = await _cursoRepository.GetByIdAsync(request.DisciplinaDto.CursoId.Value);
                if (curso == null)
                    throw new ApplicationException("O curso informado não existe.");
            }
            
            // Verificar o código, se informado
            if (request.DisciplinaDto.Codigo.HasValue && request.DisciplinaDto.Codigo.Value != disciplina.Codigo)
            {
                var codigoExistente = await _disciplinaRepository.ExistsDisciplinaByCodigoAsync(request.DisciplinaDto.Codigo.Value);
                if (codigoExistente)
                    throw new ApplicationException($"O código {request.DisciplinaDto.Codigo.Value} já está em uso por outra disciplina.");
            }
            
            // Atualizar a disciplina
            _mapper.Map(request.DisciplinaDto, disciplina);
            await _disciplinaRepository.UpdateAsync(disciplina);
            await _disciplinaRepository.SaveChangesAsync();
            
            // Atualizar pré-requisitos
            if (request.DisciplinaDto.PreRequisitosIds != null)
            {
                // Obter os pré-requisitos atuais
                var disciplinaComPreRequisitos = await _disciplinaRepository.GetDisciplinaWithPreRequisitosAsync(disciplina.Id);
                var preRequisitosAtuais = disciplinaComPreRequisitos.PreRequisitos?.Select(pr => pr.PreRequisitoId).ToList() ?? new List<int>();
                
                // Pré-requisitos a adicionar
                var preRequisitosParaAdicionar = request.DisciplinaDto.PreRequisitosIds
                    .Where(id => !preRequisitosAtuais.Contains(id))
                    .ToList();
                    
                // Pré-requisitos a remover
                var preRequisitosParaRemover = preRequisitosAtuais
                    .Where(id => !request.DisciplinaDto.PreRequisitosIds.Contains(id))
                    .ToList();
                    
                // Adicionar novos pré-requisitos
                foreach (var preRequisitoId in preRequisitosParaAdicionar)
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
                
                // Remover pré-requisitos
                foreach (var preRequisitoId in preRequisitosParaRemover)
                {
                    await _disciplinaRepository.RemovePreRequisitoAsync(disciplina.Id, preRequisitoId);
                }
            }
            
            // Atualizar professores
            if (request.DisciplinaDto.Professores != null)
            {
                // Obter os professores atuais
                var disciplinaComProfessores = await _disciplinaRepository.GetDisciplinaWithProfessoresAsync(disciplina.Id);
                var professoresAtuais = disciplinaComProfessores.Professores?.Select(p => p.ProfessorId).ToList() ?? new List<int>();
                
                // Professores a adicionar
                var professoresParaAdicionar = request.DisciplinaDto.Professores
                    .Where(p => !professoresAtuais.Contains(p.ProfessorId))
                    .ToList();
                    
                // Professores a remover
                var professoresParaRemover = professoresAtuais
                    .Where(id => !request.DisciplinaDto.Professores.Any(p => p.ProfessorId == id))
                    .ToList();
                    
                // Professores a atualizar
                var professoresParaAtualizar = request.DisciplinaDto.Professores
                    .Where(p => professoresAtuais.Contains(p.ProfessorId))
                    .ToList();
                    
                // Adicionar novos professores
                foreach (var professor in professoresParaAdicionar)
                {
                    // Verificar se o professor existe
                    var professorExistente = await _professorRepository.GetByIdAsync(professor.ProfessorId);
                    if (professorExistente == null)
                        throw new ApplicationException($"O professor com ID {professor.ProfessorId} não existe.");
                        
                    await _disciplinaRepository.AddProfessorAsync(disciplina.Id, professor.ProfessorId, professor.EhResponsavel);
                }
                
                // Remover professores
                foreach (var professorId in professoresParaRemover)
                {
                    await _disciplinaRepository.RemoveProfessorAsync(disciplina.Id, professorId);
                }
                
                // Atualizar professores
                foreach (var professor in professoresParaAtualizar)
                {
                    await _disciplinaRepository.UpdateProfessorResponsavelAsync(disciplina.Id, professor.ProfessorId, professor.EhResponsavel);
                }
            }
            
            return true;
        }
    }
}
