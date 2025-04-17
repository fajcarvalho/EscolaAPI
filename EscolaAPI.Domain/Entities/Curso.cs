using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Curso 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHorariaTotal { get; set; }

        // Relacionamentos
        public int DepartamentoId { get; set; } // Chave estrangeira
        public Departamento Departamento { get; set; } // Navegação para o departamento
        public ICollection<Disciplina> Disciplinas { get; set; } // Um curso pode ter várias disciplinas
        public ICollection<Aluno> Alunos { get; set; } // Um curso pode ter vários alunos
    }
}
