using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Departamento 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        // Relacionamentos
        public int? CordenadorId { get; set; } // Chave estrangeira opcional
        public Professor Cordenador { get; set; } // Navegação para o coordenador
        public ICollection<Professor> Professores { get; set; } // Um departamento pode ter vários professores
        public ICollection<Curso> Cursos { get; set; } // Um departamento pode ter vários cursos
    }
}
