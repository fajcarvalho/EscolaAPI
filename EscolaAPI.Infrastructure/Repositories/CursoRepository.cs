using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Infrastructure.Repositories 
{
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Curso> GetCursoWithDepartamentoAsync(int id) 
        {
            return await _context.Cursos
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Curso> GetCursoWithDisciplinasAsync(int id) 
        {
            return await _context.Cursos
                .Include(c => c.Disciplinas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Curso>> GetCursosByDepartamentoAsync(int departamentoId) 
        {
            return await _context.Cursos
                .Where(c => c.DepartamentoId == departamentoId)
                .ToListAsync();
        }

        public async Task<bool> ExistsCursoAsync(int id) {
            return await _context.Cursos.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Curso>> GetAllWithDepartamentoAsync()
        {
            return await _context.Cursos
                .Include(c => c.Departamento)
                .ToListAsync();
        }
    }
}
