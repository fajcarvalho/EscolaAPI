using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Application.Interfaces
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<IEnumerable<Professor>> GetAllWithDepartamentoAsync();
        Task<Professor> GetProfessorWithDepartamentoAsync(int id);
        Task<Professor> GetProfessorByEmailAsync(string email);
        Task<IEnumerable<Professor>> GetProfessorsByDepartamentoAsync(int departamentoId);
    }
}
