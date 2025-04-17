using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Nota 
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }

        // Relacionamentos
        public int MatriculaId { get; set; }
        public Matricula Matricula { get; set; }

        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
    }
}
