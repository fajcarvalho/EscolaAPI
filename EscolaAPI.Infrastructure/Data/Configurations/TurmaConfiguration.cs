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
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Codigo).IsRequired().HasMaxLength(20);
            builder.Property(t => t.Semestre).IsRequired().HasMaxLength(10);
            builder.Property(t => t.Ano).IsRequired();
            builder.Property(t => t.Horario).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Sala).HasMaxLength(20);
            builder.Property(t => t.CapacidadeMaxima).IsRequired();
            
            // Relacionamento N:1 com Disciplina
            builder.HasOne(t => t.Disciplina)
                  .WithMany(d => d.Turmas)
                  .HasForeignKey(t => t.DisciplinaId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            // Relacionamento N:1 com Professor
            builder.HasOne(t => t.Professor)
                  .WithMany(p => p.Turmas)
                  .HasForeignKey(t => t.ProfessorId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            // Os relacionamentos 1:N com Matricula, Aula e Avaliacao são configurados
            // nas respectivas classes de configuração
        }
    }
}