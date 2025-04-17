using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaAPI.Infrastructure.Data.Configurations
{
    public class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplinas");
            builder.HasKey(d => d.Id);
            
            builder.Property(d => d.Codigo).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Nome).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Descricao).HasMaxLength(500);
            builder.Property(d => d.CargaHoraria).IsRequired();
            
            // Relacionamento N:1 com Curso
            builder.HasOne(d => d.Curso)
                   .WithMany(c => c.Disciplinas)
                   .HasForeignKey(d => d.CursoId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            // Relacionamento 1:N com Turma
            builder.HasMany(d => d.Turmas)
                   .WithOne(t => t.Disciplina)
                   .HasForeignKey(t => t.DisciplinaId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamentos N:N com Professor já configurado em ProfessorDisciplinaConfiguration
            
            // Relacionamentos N:N para pré-requisitos já configurado em DisciplinaPreRequisitoConfiguration
        }
    }
}