using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.Infrastructure.Repositories 
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(ApplicationDbContext context) : base(context) 
        {
        }
        public async Task<Aluno> GetAlunoWithEnderecoAsync(int id) 
        {
            return await _context.Alunos
                .Include(a => a.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Aluno> GetAlunoByMatriculaAsync(string matricula) 
        {
            return await _context.Alunos
                .Include(a => a.Endereco)
                .FirstOrDefaultAsync(a => a.Matricula == matricula);
        }
    }
}
