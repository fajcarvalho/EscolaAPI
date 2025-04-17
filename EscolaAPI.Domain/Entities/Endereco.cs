using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAPI.Domain.Entities 
{
    public class Endereco 
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        // Relacionamento 1:1 com Aluno
        public int AlunoId { get; set; } // Chave estrangeira
        public Aluno Aluno { get; set; } // Navegação para o aluno
    }
}
