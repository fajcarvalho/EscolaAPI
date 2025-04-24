namespace EscolaAPI.Application.DTOs
{
    public class DepartamentoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? CordenadorId { get; set; }
    }
    
    public class CreateDepartamentoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? CordenadorId { get; set; }
    }
    
    public class UpdateDepartamentoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? CordenadorId { get; set; }
    }
}