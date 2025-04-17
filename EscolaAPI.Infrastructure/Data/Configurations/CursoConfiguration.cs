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
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Descricao).HasMaxLength(500);
            builder.Property(c => c.CargaHorariaTotal).IsRequired();
            
            // Relacionamento N:1 com Departamento
            builder.HasOne(c => c.Departamento)
                  .WithMany(d => d.Cursos)
                  .HasForeignKey(c => c.DepartamentoId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            // Relacionamento 1:N com Disciplina
            builder.HasMany(c => c.Disciplinas)
                  .WithOne(d => d.Curso)
                  .HasForeignKey(d => d.CursoId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
