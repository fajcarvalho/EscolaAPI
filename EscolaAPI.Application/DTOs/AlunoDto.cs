using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Enums;

namespace EscolaAPI.Application.DTOs 
{
    public class AlunoDto 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataIngresso { get; set; }
        public StatusAluno Status { get; set; } // Enum Ativo, Trancado, Formado

        // DTO para o relacionamento 1:1 com Endereco
        public EnderecoDto Endereco { get; set; }
    }

    public class EnderecoDto
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }

    public class CreateAlunoDto 
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataIngresso { get; set; }
        public StatusAluno Status { get; set; } // Enum Ativo, Trancado, Formado

        public CreateEnderecoDto Endereco { get; set; }
    }

    public class CreateEnderecoDto 
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }

    public class UpdateAlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public StatusAluno Status { get; set; }
        
        public UpdateEnderecoDto Endereco { get; set; }
    }

    public class UpdateEnderecoDto
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}
