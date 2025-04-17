using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class ItemHistorico 
    {
        public int Id { get; set; }
        public string Semestre { get; set; }
        public int Ano { get; set; }
        public decimal NotaFinal { get; set; }
        public string Observacao { get; set; }

        // Relacionamentos
        public int HistoricoId { get; set; }
        public Historico Historico { get; set; }

        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
