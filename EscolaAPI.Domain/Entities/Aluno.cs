using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Enums;

namespace EscolaAPI.Domain.Entities 
{
    public class Aluno 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataIngresso { get; set; }
        public StatusAluno Status { get; set; } // Enum Ativo, Trancado, Formado

        // Relacionamentos
        public Endereco Endereco { get; set; }
        public Historico Historico { get; set; }
        public ICollection<Matricula> Matriculas { get; set; }
    }
}
