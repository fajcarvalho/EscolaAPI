using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.DTOs;
using FluentValidation;

namespace EscolaAPI.Application.Validators.Aluno 
{
    public class CreateAlunoDtoValidator : AbstractValidator<CreateAlunoDto> 
    {
        public CreateAlunoDtoValidator() 
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome não pode ter mais de 100 caracteres");

            RuleFor(x => x.Matricula)
                .NotEmpty()
                .WithMessage("A matrícula é obrigatória.")
                .MaximumLength(20)
                .WithMessage("A matrícula não pode ter mais de 20 caracteres");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now)
                .WithMessage("A data de nascimento deve ser anterior a data atual.");

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

            RuleFor(x => x.DataIngresso)
                .NotEmpty()
                .WithMessage("A data de ingresso é obrigatória");

            // Validação do status com opções válidas do enum 
            var statusOptions = Enum.GetNames(typeof(Domain.Enums.StatusAluno))
                                    .Select(name => $"'{name}'")
                                    .ToArray();
            var optionsText = string.Join(", ", statusOptions);
            var statusErrorMessage = $"Status inválido. Opções válidas: {optionsText}";

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(_ => statusErrorMessage);

            RuleFor(x => x.Endereco)
                .NotNull()
                .WithMessage("O endereço é obrigatório.")
                .SetValidator(new CreateEnderecoDtoValidator());
        }
    }

    public class CreateEnderecoDtoValidator : AbstractValidator<CreateEnderecoDto>
    {
        public CreateEnderecoDtoValidator()
        {
            RuleFor(x => x.Rua)
                .NotEmpty()
                .WithMessage("A rua é obrigatória")
                .MaximumLength(100)
                .WithMessage("A rua não pode ter mais de 100 caracteres");
                
            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("O número é obrigatório")
                .MaximumLength(10)
                .WithMessage("O número não pode ter mais de 10 caracteres");
                
            RuleFor(x => x.Bairro)
                .NotEmpty()
                .WithMessage("O bairro é obrigatório")
                .MaximumLength(50)
                .WithMessage("O bairro não pode ter mais de 50 caracteres");
                
            RuleFor(x => x.Cidade)
                .NotEmpty()
                .WithMessage("A cidade é obrigatória")
                .MaximumLength(50)
                .WithMessage("A cidade não pode ter mais de 50 caracteres");
                
            RuleFor(x => x.Estado)
                .NotEmpty()
                .WithMessage("O estado é obrigatório")
                .Length(2)
                .WithMessage("O estado deve ter 2 caracteres");
                
            RuleFor(x => x.CEP)
                .NotEmpty()
                .WithMessage("O CEP é obrigatório")
                .MaximumLength(10)
                .WithMessage("O CEP não pode ter mais de 10 caracteres");
        }
    }
}
