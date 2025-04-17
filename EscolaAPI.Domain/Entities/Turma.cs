using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Turma 
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Semestre { get; set; }
        public int Ano { get; set; }
        public int Horario { get; set; }
        public int Sala { get; set; }

        public int CapacidadeMaxima { get; set; }

        // Relacionamentos
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }
        public ICollection<Aula> Aulas { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
    }
}
