using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Frequencia 
    {
        public int Id { get; set; }
        public bool Presente { get; set; }
        public string Observacao { get; set; }

        // Relacionamentos
        public int MatriculaId { get; set; }
        public Matricula Matricula { get; set; }

        public int AulaId { get; set; }
        public Aula Aula { get; set; }
    }
}
