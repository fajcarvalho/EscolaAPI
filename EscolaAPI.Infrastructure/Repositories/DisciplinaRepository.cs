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
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(ApplicationDbContext context) : base(context) 
        {
        }
        public async Task<Disciplina> GetDisciplinaWithCursoAsync(int id)
        {
            return await _context.Disciplinas
                .Include(d => d.Curso)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Disciplina> GetDisciplinaWithPreRequisitosAsync(int id) 
        {
            return await _context.Disciplinas
                .Include(d => d.PreRequisitos)
                    .ThenInclude(pr => pr.PreRequisito)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Disciplina> GetDisciplinaWithProfessoresAsync(int id) 
        {
            return await _context.Disciplinas
                .Include(d => d.Professores)
                    .ThenInclude(pd => pd.Professor)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Disciplina> GetDisciplinaWithAllDetailsAsync(int id)
        {
            return await _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.PreRequisitos)
                    .ThenInclude(pr => pr.PreRequisito)
                .Include(d => d.Professores)
                    .ThenInclude(pd => pd.Professor)
                .Include(d => d.Turmas)
                .Include(d => d.DisciplinasQuePreRequisitamEsta)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Disciplina>> GetDisciplinasByCursoAsync(int cursoId) 
        {
            return await _context.Disciplinas
                .Where(d => d.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Disciplina>> GetAllWithDetailsAsync()
        {
            return await _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.PreRequisitos)
                    .ThenInclude(pr => pr.PreRequisito)
                .Include(d => d.Professores)
                    .ThenInclude(pd => pd.Professor)
                .Include(d => d.Turmas)
                .ToListAsync();
        }

        public async Task<bool> ExistsDisciplinaAsync(int id) 
        {
            return await _context.Disciplinas.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> ExistsDisciplinaByCodigoAsync(int codigo) 
        {
            return await _context.Disciplinas.AnyAsync(d => d.Codigo == codigo);
        }

        public async Task AddPreRequisitoAsync(int disciplinaId, int preRequisitoId)
        {
            // Verificar se já existe o pré-requisito
            var existingPreRequisito = await _context.DisciplinaPreRequisitos
                .FirstOrDefaultAsync(dp => dp.DisciplinaId == disciplinaId && dp.PreRequisitoId == preRequisitoId);
                
            if (existingPreRequisito == null)
            {
                var disciplinaPreRequisito = new DisciplinaPreRequisito
                {
                    DisciplinaId = disciplinaId,
                    PreRequisitoId = preRequisitoId
                };
                
                await _context.DisciplinaPreRequisitos.AddAsync(disciplinaPreRequisito);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemovePreRequisitoAsync(int disciplinaId, int preRequisitoId)
        {
            var disciplinaPreRequisito = await _context.DisciplinaPreRequisitos
                .FirstOrDefaultAsync(dp => dp.DisciplinaId == disciplinaId && dp.PreRequisitoId == preRequisitoId);
                
            if (disciplinaPreRequisito != null)
            {
                _context.DisciplinaPreRequisitos.Remove(disciplinaPreRequisito);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddProfessorAsync(int disciplinaId, int professorId, bool ehResponsavel)
        {
            // Verificar se já existe a associação
            var existingAssociacao = await _context.ProfessoresDisciplinas
                .FirstOrDefaultAsync(pd => pd.DisciplinaId == disciplinaId && pd.ProfessorId == professorId);
                
            if (existingAssociacao == null)
            {
                var professorDisciplina = new ProfessorDisciplina
                {
                    DisciplinaId = disciplinaId,
                    ProfessorId = professorId,
                    DataHabilitacao = DateTime.Now,
                    EhResponsavel = ehResponsavel
                };
                
                await _context.ProfessoresDisciplinas.AddAsync(professorDisciplina);
                await _context.SaveChangesAsync();
            }
            else if (existingAssociacao.EhResponsavel != ehResponsavel)
            {
                // Atualizar o status de responsável
                existingAssociacao.EhResponsavel = ehResponsavel;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveProfessorAsync(int disciplinaId, int professorId)
        {
            var professorDisciplina = await _context.ProfessoresDisciplinas
                .FirstOrDefaultAsync(pd => pd.DisciplinaId == disciplinaId && pd.ProfessorId == professorId);
                
            if (professorDisciplina != null)
            {
                _context.ProfessoresDisciplinas.Remove(professorDisciplina);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProfessorResponsavelAsync(int disciplinaId, int professorId, bool ehResponsavel)
        {
            var professorDisciplina = await _context.ProfessoresDisciplinas
                .FirstOrDefaultAsync(pd => pd.DisciplinaId == disciplinaId && pd.ProfessorId == professorId);
                
            if (professorDisciplina != null)
            {
                professorDisciplina.EhResponsavel = ehResponsavel;
                await _context.SaveChangesAsync();
            }
        }
    }
}
