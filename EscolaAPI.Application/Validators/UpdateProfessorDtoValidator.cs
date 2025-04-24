using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;


namespace EscolaAPI.Application.Validators
{
    public class UpdateProfessorDtoValidator : AbstractValidator<UpdateProfessorDto>
    {
         private readonly IDepartamentoRepository _departamentoRepository;

        
        public UpdateProfessorDtoValidator(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
            
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID inválido");
                
            RuleFor(x => x.Nome)
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Nome));
                
            RuleFor(x => x.Titulacao)
                .MaximumLength(50).WithMessage("A titulação não pode ter mais de 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Titulacao));
                
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email inválido")
                .MaximumLength(100).WithMessage("O email não pode ter mais de 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Email));
                
            RuleFor(x => x.Telefone)
                .MaximumLength(15).WithMessage("O telefone não pode ter mais de 15 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Telefone));
                
            // Validação do status com opções válidas do enum 
            var statusOptions = Enum.GetNames(typeof(EscolaAPI.Domain.Enums.StatusProfessor))
                                   .Select(name => $"'{name}'")
                                   .ToArray();
            var optionsText = string.Join(", ", statusOptions);
            var statusErrorMessage = $"Status inválido. Opções válidas: {optionsText}";

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(_ => statusErrorMessage);
                
            RuleFor(x => x.DepartamentoId)
                .MustAsync((id, cancellationToken) => DepartamentoExisteAsync(id.Value, cancellationToken))
                .WithMessage("O departamento informado não existe.")
                .When(x => x.DepartamentoId.HasValue);
        }

        private async Task<bool> DepartamentoExisteAsync(int departamentoId, CancellationToken cancellationToken)
        {
            return await _departamentoRepository.ExistsDepartamentoAsync(departamentoId);
        }
    }
}