using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class ProfessorDisciplina 
    {
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } // Navegação para o professor

        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } // Navegação para a disciplina

        public DateTime DataHabilitacao { get; set; }
        public bool EhResponsavel { get; set; } // Indica se o professor é responsável pela disciplina
    }
}
