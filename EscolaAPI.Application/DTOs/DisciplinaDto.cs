using EscolaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Application.DTOs {
    public class DisciplinaDto 
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; } 
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }

        public int CursoId { get; set; }
        public string CursoNome { get; set; }
        public int QuantidadeTurmas { get; set; }
        public List<PreRequisitoDisciplinaDto> PreRequisitos { get; set; } 
        public List<ProfessorDisciplinaDto> Professores { get; set; }
    }

    public class PreRequisitoDisciplinaDto 
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }

    public class ProfessorDisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Titulacao { get; set; }
        public bool EhResponsavel { get; set; }
    }

    public class CreateDisciplinaDto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int CursoId { get; set; }
        public List<int> PreRequisitosIds { get; set; } = new List<int>();
        public List<ProfessorDisciplinaParaAssociarDto> Professores { get; set; } = new List<ProfessorDisciplinaParaAssociarDto>();
    }

    public class ProfessorDisciplinaParaAssociarDto
    {
            public int ProfessorId { get; set; }
            public bool EhResponsavel { get; set; }
    }

    public class UpdateDisciplinaDto
    {
        public int Id { get; set; }
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? CargaHoraria { get; set; }
        public int? CursoId { get; set; }
        public List<int> PreRequisitosIds { get; set; } = new List<int>();
        public List<ProfessorDisciplinaParaAssociarDto> Professores { get; set; } = new List<ProfessorDisciplinaParaAssociarDto>();
    }
}
