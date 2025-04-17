using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Disciplina 
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }

        // Relacionamentos
        public int CursoId { get; set; } // Chave estrangeira
        public Curso Curso { get; set; } // Navegação para o curso
        public ICollection<Turma> Turmas { get; set; } // Uma disciplina pode ter várias turmas

        // Relacionamento N:N com outras disciplinas (pré-requisitos)
        public ICollection<DisciplinaPreRequisito> PreRequisitos { get; set; } // Coleção de pré-requisitos
        public ICollection<DisciplinaPreRequisito> DisciplinasQuePreRequisitamEsta { get; set; } // Coleção de disciplinas que pre-requisitam esta
        public ICollection<ProfessorDisciplina> Professores { get; set; } // Coleção de professores que lecionam esta disciplina
    }
}
