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
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("Departamentos");
            builder.HasKey(d => d.Id);
            
            builder.Property(d => d.Nome).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Descricao).HasMaxLength(500);
            
            // Relacionamento 1:1 com Professor (como Cordenador)
            builder.HasOne(d => d.Cordenador)
                  .WithMany()  // Não há propriedade de navegação no Professor para Departamentos coordenados
                  .HasForeignKey(d => d.CordenadorId)
                  .IsRequired(false)  // Pode não ter coordenador por um período
                  .OnDelete(DeleteBehavior.Restrict);
            
            // Relacionamento 1:N com Professor já configurado em ProfessorConfiguration
            
            // Relacionamento 1:N com Curso
            builder.HasMany(d => d.Cursos)
                  .WithOne(c => c.Departamento)
                  .HasForeignKey(c => c.DepartamentoId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}