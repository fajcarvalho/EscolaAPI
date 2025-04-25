using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;

namespace EscolaAPI.Application.Validators
{
    public class CreateDisciplinaDtoValidator : AbstractValidator<CreateDisciplinaDto>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IProfessorRepository _professorRepository;
        
        public CreateDisciplinaDtoValidator(
            ICursoRepository cursoRepository,
            IProfessorRepository professorRepository)
        {
            _cursoRepository = cursoRepository;
            _professorRepository = professorRepository;
            
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");
                
            RuleFor(x => x.Codigo)
                .GreaterThan(0).WithMessage("O código deve ser maior que zero");
                
            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres");
                
            RuleFor(x => x.CargaHoraria)
                .GreaterThan(0).WithMessage("A carga horária deve ser maior que zero");
                
            RuleFor(x => x.CursoId)
                .GreaterThan(0).WithMessage("Curso inválido")
                .MustAsync(CursoExistsAsync).WithMessage("O curso informado não existe.");
                
            When(x => x.PreRequisitosIds != null && x.PreRequisitosIds.Any(), () => {
                RuleForEach(x => x.PreRequisitosIds)
                    .GreaterThan(0).WithMessage("ID de pré-requisito inválido");
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
        
        private async Task<bool> CursoExistsAsync(int cursoId, CancellationToken cancellationToken)
        {
            return await _cursoRepository.ExistsCursoAsync(cursoId);
        }
        
        private async Task<bool> ProfessorExistsAsync(int professorId, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId);
            return professor != null;
        }
    }
}