using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Especializacao 
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Descricao { get; set; }

        // Relacionamentos
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}
