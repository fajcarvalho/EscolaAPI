using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class DisciplinaPreRequisito 
    {
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } // Navegação para a disciplina

        public int PreRequisitoId { get; set; }
        public Disciplina PreRequisito { get; set; } // Navegação para o pré-requisito
    }
}
