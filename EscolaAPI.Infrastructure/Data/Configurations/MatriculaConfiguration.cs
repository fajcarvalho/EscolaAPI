using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EscolaAPI.Domain.Enums;

namespace EscolaAPI.Infrastructure.Data.Configurations
{
    public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.DataMatricula).IsRequired();
            
            // Configuração do enum
            builder.Property(m => m.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);
            
            // Relacionamento N:1 com Aluno
            builder.HasOne(m => m.Aluno)
                  .WithMany(a => a.Matriculas)
                  .HasForeignKey(m => m.AlunoId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamento N:1 com Turma
            builder.HasOne(m => m.Turma)
                  .WithMany(t => t.Matriculas)
                  .HasForeignKey(m => m.TurmaId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            // Relacionamento 1:N com Nota
            builder.HasMany(m => m.Notas)
                  .WithOne(n => n.Matricula)
                  .HasForeignKey(n => n.MatriculaId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamento 1:N com Frequencia
            builder.HasMany(m => m.Frequencias)
                  .WithOne(f => f.Matricula)
                  .HasForeignKey(f => f.MatriculaId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}