using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Application.Interfaces
{
    public interface IDepartamentoRepository : IRepository<Departamento>
    {
        Task<Departamento> GetDepartamentoWithProfessoresAsync(int id);
        Task<Departamento> GetDepartamentoWithCursosAsync(int id);
        Task<Departamento> GetDepartamentoWithCordenadorAsync(int id);
        Task<IEnumerable<Departamento>> GetAllDepartamentosWithDetailsAsync();
        Task<bool> ExistsDepartamentoAsync(int id);
    }
}