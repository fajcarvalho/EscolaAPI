using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using FluentValidation;

namespace EscolaAPI.Application.Validators 
{
    public class UpdateAlunoDtoValidator : AbstractValidator<UpdateAlunoDto>
    {
        public UpdateAlunoDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID inválido");
            
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome não pode ter mais de 100 caracteres");

            RuleFor(x => x.Email)
                .MaximumLength(100)
                .WithMessage("O email não pode ter mais de 100 caracteres")
                .EmailAddress()
                .WithMessage("O email deve ser um endereço de email válido.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Telefone)
                .MaximumLength(15)
                .WithMessage("O telefone não pode ter mais de 15 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Telefone));

            // Validação do status com opções válidas do enum 
            var statusOptions = Enum.GetNames(typeof(EscolaAPI.Domain.Enums.StatusAluno))
                                    .Select(name => $"'{name}'")
                                    .ToArray();
            var optionsText = string.Join(", ", statusOptions);
            var statusErrorMessage = $"Status inválido. Opções válidas: {optionsText}";
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(_ => statusErrorMessage);

            RuleFor(x => x.Endereco)
                .NotNull()
                .SetValidator(new UpdateEnderecoDtoValidator())
                .When(x => x.Endereco != null);
        }
    }

    public class UpdateEnderecoDtoValidator : AbstractValidator<UpdateEnderecoDto> 
    {
        public UpdateEnderecoDtoValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID inválido");
                
            RuleFor(x => x.Rua)
                .MaximumLength(100)
                .WithMessage("A rua não pode ter mais de 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Rua));
                
            RuleFor(x => x.Numero)
                .MaximumLength(10)
                .WithMessage("O número não pode ter mais de 10 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Numero));
                
            RuleFor(x => x.Bairro)
                .MaximumLength(50)
                .WithMessage("O bairro não pode ter mais de 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Bairro));
                
            RuleFor(x => x.Cidade)
                .MaximumLength(50)
                .WithMessage("A cidade não pode ter mais de 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Cidade));
                
            RuleFor(x => x.Estado)
                .Length(2)
                .WithMessage("O estado deve ter 2 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Estado));
                
            RuleFor(x => x.CEP)
                .MaximumLength(10)
                .WithMessage("O CEP não pode ter mais de 10 caracteres")
                .When(x => !string.IsNullOrEmpty(x.CEP));
        }
    }
}
