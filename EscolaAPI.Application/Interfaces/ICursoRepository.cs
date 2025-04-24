using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Application.Interfaces 
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Task<Curso> GetCursoWithDepartamentoAsync(int id);
        Task<Curso> GetCursoWithDisciplinasAsync(int id);
        Task<IEnumerable<Curso>> GetCursosByDepartamentoAsync(int departamentoId);
        Task<bool> ExistsCursoAsync(int id);
        Task<IEnumerable<Curso>> GetAllWithDepartamentoAsync();
    }
}
