using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Infrastructure.Data 
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
        }

        // DbSet para cada entidade
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Especializacao> Especializacoes { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<ItemHistorico> ItensHistorico { get; set; }
        public DbSet<ProfessorDisciplina> ProfessoresDisciplinas { get; set; }
        public DbSet<DisciplinaPreRequisito> DisciplinaPreRequisitos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
