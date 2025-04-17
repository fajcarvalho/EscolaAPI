using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Enums;

namespace EscolaAPI.Domain.Entities 
{
    public class Professor 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Titulacao { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataContratacao { get; set; }
        public StatusProfessor Status { get; set; } // Ativo, Afastado, Aposentado
        
        // Relacionamentos
        public int DepartamentoId { get; set; } // Chave estrangeira
        public Departamento Departamento { get; set; } // Navegação para o departamento
        public ICollection<Turma> Turmas { get; set; } // Professor pode lecionar em várias turmas
        public ICollection<Especializacao> Especializacoes { get; set; } // Professor pode ter várias especializações
        public ICollection<ProfessorDisciplina> Disciplinas { get; set; } // Professor pode lecionar várias disciplinas
    }
}
