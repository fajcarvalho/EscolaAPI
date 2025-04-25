using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Application.Interfaces
{
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<Disciplina> GetDisciplinaWithCursoAsync(int id);
        Task<Disciplina> GetDisciplinaWithPreRequisitosAsync(int id);
        Task<Disciplina> GetDisciplinaWithProfessoresAsync(int id);
        Task<Disciplina> GetDisciplinaWithAllDetailsAsync(int id);
        Task<IEnumerable<Disciplina>> GetDisciplinasByCursoAsync(int cursoId);
        Task<IEnumerable<Disciplina>> GetAllWithDetailsAsync();
        Task<bool> ExistsDisciplinaAsync(int id);
        Task<bool> ExistsDisciplinaByCodigoAsync(int codigo);
        Task AddPreRequisitoAsync(int disciplinaId, int preRequisitoId);
        Task RemovePreRequisitoAsync(int disciplinaId, int preRequisitoId);
        Task AddProfessorAsync(int disciplinaId, int professorId, bool ehResponsavel);
        Task RemoveProfessorAsync(int disciplinaId, int professorId);
        Task UpdateProfessorResponsavelAsync(int disciplinaId, int professorId, bool ehResponsavel);
    }
}
