using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Domain.Entities;
using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Infrastructure.Repositories
{
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Departamento> GetDepartamentoWithProfessoresAsync(int id)
        {
            return await _context.Departamentos
                .Include(d => d.Professores)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Departamento> GetDepartamentoWithCursosAsync(int id)
        {
            return await _context.Departamentos
                .Include(d => d.Cursos)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Departamento> GetDepartamentoWithCordenadorAsync(int id)
        {
            return await _context.Departamentos
                .Include(d => d.Cordenador)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Departamento>> GetAllDepartamentosWithDetailsAsync()
        {
            return await _context.Departamentos
                .Include(d => d.Professores)
                .Include(d => d.Cursos)
                .ToListAsync();
        }

        public async Task<bool> ExistsDepartamentoAsync(int id)
        {
            return await _context.Departamentos.AnyAsync(d => d.Id == id);
        }
    }
}