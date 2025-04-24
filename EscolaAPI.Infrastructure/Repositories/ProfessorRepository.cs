using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Infrastructure.Repositories
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Professor> GetProfessorWithDepartamentoAsync(int id)
        {
            return await _context.Professores
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Professor> GetProfessorByEmailAsync(string email)
        {
            return await _context.Professores
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<Professor>> GetProfessorsByDepartamentoAsync(int departamentoId)
        {
            return await _context.Professores
                .Where(p => p.DepartamentoId == departamentoId)
                .ToListAsync();
        }
    }
}
