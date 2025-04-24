using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;

namespace EscolaAPI.Application.Validators.Departamento
{
    public class CreateDepartamentoDtoValidator : AbstractValidator<CreateDepartamentoDto>
    {
        private readonly IProfessorRepository _professorRepository;
        
        public CreateDepartamentoDtoValidator(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
            
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");
                
            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres");
                
            RuleFor(x => x.CordenadorId)
                .MustAsync(ProfessorExistsAsync)
                .When(x => x.CordenadorId.HasValue)
                .WithMessage("O professor indicado como coordenador não existe.");
        }
        
        private async Task<bool> ProfessorExistsAsync(int? professorId, CancellationToken cancellationToken)
        {
            if (!professorId.HasValue) return true;
            
            return await _professorRepository.GetByIdAsync(professorId.Value) != null;
        }
    }
}