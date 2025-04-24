using System;
using System.Collections.Generic;
using EscolaAPI.Domain.Enums;

namespace EscolaAPI.Application.DTOs
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Titulacao { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataContratacao { get; set; }
        public StatusProfessor Status { get; set; }
        
        public int DepartamentoId { get; set; }
        public string DepartamentoNome { get; set; }
    }
    
    public class CreateProfessorDto
    {
        public string Nome { get; set; }
        public string Titulacao { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataContratacao { get; set; }
        public StatusProfessor Status { get; set; }
        public int DepartamentoId { get; set; }
    }
    
    public class UpdateProfessorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Titulacao { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public StatusProfessor Status { get; set; }
        public int? DepartamentoId { get; set; }
    }
}