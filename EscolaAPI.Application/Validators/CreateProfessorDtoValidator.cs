using System;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using FluentValidation;

namespace EscolaAPI.Application.Validators
{
    public class CreateProfessorDtoValidator : AbstractValidator<CreateProfessorDto> 
    {

        private readonly IDepartamentoRepository _departamentoRepository;

        public CreateProfessorDtoValidator(IDepartamentoRepository departamentoRepository) 
        {
            _departamentoRepository = departamentoRepository;
            
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");

            RuleFor(x => x.Titulacao)
                .NotEmpty().WithMessage("A titulação é obrigatória")
                .MaximumLength(50).WithMessage("A titulação não pode ter mais de 50 caracteres");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória")
                .LessThan(DateTime.Now.AddYears(-18)).WithMessage("O professor deve ter pelo menos 18 anos");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("Email inválido")
                .MaximumLength(100).WithMessage("O email não pode ter mais de 100 caracteres");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório")
                .MaximumLength(15).WithMessage("O telefone não pode ter mais de 15 caracteres");

            RuleFor(x => x.DataContratacao)
                .NotEmpty().WithMessage("A data de contratação é obrigatória")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de contratação não pode ser futura");

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
                .GreaterThan(0).WithMessage("Departamento inválido")
                .MustAsync(DepartamentoExisteAsync).WithMessage("O departamento informado não existe.");
        }
        private async Task<bool> DepartamentoExisteAsync(int departamentoId, CancellationToken cancellationToken)
        {
            return await _departamentoRepository.ExistsDepartamentoAsync(departamentoId);
        }
    }
}