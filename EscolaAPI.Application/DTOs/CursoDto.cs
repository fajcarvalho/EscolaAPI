using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Application.DTOs 
{
    public class CursoDto 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHorariaTotal { get; set; }

        public int DepartamentoId { get; set; }
        public string DepartamentoNome { get; set; }
        public int QuantidadeDisciplinas { get; set; }
    }

    public class CreateCursoDto 
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHorariaTotal { get; set; }
        public int DepartamentoId { get; set; }
    }

    public class UpdateCursoDto 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? CargaHorariaTotal { get; set; }
        public int? DepartamentoId { get; set; }
    }
}