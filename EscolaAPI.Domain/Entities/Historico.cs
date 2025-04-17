using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Historico 
    {
        public int Id { get; set; }
        public DateTime DataGeracao { get; set; }

        // Relacionamentos (1:1 com Aluno)
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public ICollection<ItemHistorico> Itens { get; set; }
    }
}
