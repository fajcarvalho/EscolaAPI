using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;

namespace EscolaAPI.Application.Validators
{
    public class CreateCursoDtoValidator : AbstractValidator<CreateCursoDto>
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        
        public CreateCursoDtoValidator(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
            
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");
                
            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres");
                
            RuleFor(x => x.CargaHorariaTotal)
                .GreaterThan(0).WithMessage("A carga horária total deve ser maior que zero");
                
            RuleFor(x => x.DepartamentoId)
                .GreaterThan(0).WithMessage("Departamento inválido")
                .MustAsync(DepartamentoExistsAsync).WithMessage("O departamento informado não existe.");
        }
        
        private async Task<bool> DepartamentoExistsAsync(int departamentoId, CancellationToken cancellationToken)
        {
            return await _departamentoRepository.ExistsDepartamentoAsync(departamentoId);
        }
    }
}