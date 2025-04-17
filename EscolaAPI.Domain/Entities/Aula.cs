using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Aula 
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Conteudo { get; set; }

        // Relacionamentos
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public ICollection<Frequencia> Frequencias { get; set; }
    }
}
