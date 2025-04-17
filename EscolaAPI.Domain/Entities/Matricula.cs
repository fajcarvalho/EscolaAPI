using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Enums;

namespace EscolaAPI.Domain.Entities 
{
    public class Matricula 
    {
        public int Id { get; set; }
        public DateTime DataMatricula { get; set; }
        public StatusMatricula Status { get; set; }

        // Relacionamentos
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public ICollection<Nota> Notas { get; set; }
        public ICollection<Frequencia> Frequencias { get; set; }
    }
}
