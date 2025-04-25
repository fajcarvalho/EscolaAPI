using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;

namespace EscolaAPI.Application.Validators
{
    public class UpdateDisciplinaDtoValidator : AbstractValidator<UpdateDisciplinaDto>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        
        public UpdateDisciplinaDtoValidator(
            ICursoRepository cursoRepository,
            IProfessorRepository professorRepository,
            IDisciplinaRepository disciplinaRepository)
        {
            _cursoRepository = cursoRepository;
            _professorRepository = professorRepository;
            _disciplinaRepository = disciplinaRepository;
            
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID inválido")
                .MustAsync(DisciplinaExistsAsync).WithMessage("A disciplina informada não existe.");
                
            RuleFor(x => x.Nome)
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Nome));
                
            RuleFor(x => x.Codigo)
                .GreaterThan(0).WithMessage("O código deve ser maior que zero")
                .When(x => x.Codigo.HasValue);
                
            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Descricao));
                
            RuleFor(x => x.CargaHoraria)
                .GreaterThan(0).WithMessage("A carga horária deve ser maior que zero")
                .When(x => x.CargaHoraria.HasValue);
                
            RuleFor(x => x.CursoId)
                .GreaterThan(0).WithMessage("Curso inválido")
                .MustAsync(CursoExistsAsync).WithMessage("O curso informado não existe.")
                .When(x => x.CursoId.HasValue);
                
            When(x => x.PreRequisitosIds != null && x.PreRequisitosIds.Any(), () => {
                RuleForEach(x => x.PreRequisitosIds)
                    .GreaterThan(0).WithMessage("ID de pré-requisito inválido");
                
                // Verificar se algum pré-requisito é a própria disciplina
                RuleFor(x => x)
                    .Must(x => !x.PreRequisitosIds.Contains(x.Id))
                    .WithMessage("Uma disciplina não pode ser pré-requisito dela mesma.");
            });
            
            When(x => x.Professores != null && x.Professores.Any(), () => {
                RuleForEach(x => x.Professores).ChildRules(professor => {
                    professor.RuleFor(p => p.ProfessorId)
                        .GreaterThan(0).WithMessage("ID de professor inválido")
                        .MustAsync(ProfessorExistsAsync).WithMessage((p, id) => $"O professor com ID {id} não existe.");
                });
                
                // Verificar se há mais de um professor responsável
                RuleFor(x => x.Professores)
                    .Must(professores => professores.Count(p => p.EhResponsavel) <= 1)
                    .WithMessage("Uma disciplina pode ter no máximo um professor responsável.");
            });
        }
        
        private async Task<bool> DisciplinaExistsAsync(int disciplinaId, CancellationToken cancellationToken)
        {
            return await _disciplinaRepository.ExistsDisciplinaAsync(disciplinaId);
        }
        
        private async Task<bool> CursoExistsAsync(int? cursoId, CancellationToken cancellationToken)
        {
            if (!cursoId.HasValue) return true;
            
            return await _cursoRepository.ExistsCursoAsync(cursoId.Value);
        }
        
        private async Task<bool> ProfessorExistsAsync(int professorId, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId);
            return professor != null;
        }
    }
}