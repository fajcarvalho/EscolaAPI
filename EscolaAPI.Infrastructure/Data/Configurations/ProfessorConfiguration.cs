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
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            // Configuração básica
            builder.ToTable("Professores");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Titulacao).HasMaxLength(50);
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.Telefone).HasMaxLength(15);
            builder.Property(p => p.DataContratacao).IsRequired();

            // Configuração do enum
            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            // Relacionamento N:1 com Departamento
            builder.HasOne(p => p.Departamento)
                   .WithMany(d => d.Professores)
                   .HasForeignKey(p => p.DepartamentoId)
                   .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata

            // Relacionamento 1:N com Especializacao
            builder.HasMany(p => p.Especializacoes)
                   .WithOne(e => e.Professor)
                   .HasForeignKey(e => e.ProfessorId)
                   .OnDelete(DeleteBehavior.Cascade); // Permitir exclusão em cascata

        }
    }
}
