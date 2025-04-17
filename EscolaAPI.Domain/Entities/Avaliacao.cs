using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Avaliacao 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Peso { get; set; }

        // Relacionamentos
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public ICollection<Nota> Notas { get; set; }
    }
}
