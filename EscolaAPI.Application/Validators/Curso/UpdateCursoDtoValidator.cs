using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;

namespace EscolaAPI.Application.Validators
{
    public class UpdateCursoDtoValidator : AbstractValidator<UpdateCursoDto>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        
        public UpdateCursoDtoValidator(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
            
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID inválido");
                
            RuleFor(x => x.Nome)
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Nome));
                
            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Descricao));
                
            RuleFor(x => x.CargaHorariaTotal)
                .GreaterThan(0).WithMessage("A carga horária total deve ser maior que zero")
                .When(x => x.CargaHorariaTotal.HasValue);
                
            RuleFor(x => x.DepartamentoId)
                .MustAsync(DepartamentoExistsAsync).WithMessage("O departamento informado não existe.")
                .When(x => x.DepartamentoId.HasValue);
        }
        
        private async Task<bool> DepartamentoExistsAsync(int? departamentoId, CancellationToken cancellationToken)
        {
            if (!departamentoId.HasValue) return true;
            
            return await _departamentoRepository.ExistsDepartamentoAsync(departamentoId.Value);
        }
    }
}