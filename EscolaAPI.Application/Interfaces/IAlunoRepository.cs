using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Application.Interfaces 
{
    public interface IAlunoRepository : IRepository<Aluno> 
    {
        Task<Aluno> GetAlunoWithEnderecoAsync(int id);
        Task<Aluno> GetAlunoByMatriculaAsync(string matricula);
    }
}
