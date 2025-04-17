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
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder) 
        {
            // Configuração básica
            builder.ToTable("Alunos");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Matricula).IsRequired().HasMaxLength(20);
            builder.Property(a => a.DataNascimento).IsRequired();
            builder.Property(a => a.Email).HasMaxLength(100);
            builder.Property(a => a.Telefone).HasMaxLength(15);

            // Configuração do enum
            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Relacionamento 1:1 com Endereco
            builder.HasOne(a => a.Endereco)
                .WithOne(e => e.Aluno)
                .HasForeignKey<Endereco>(e => e.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:1 com Historico
            builder.HasOne(a => a.Historico)
                .WithOne(h => h.Aluno)
                .HasForeignKey<Historico>(h => h.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:N com Matricula
            builder.HasMany(a => a.Matriculas)
                .WithOne(m => m.Aluno)
                .HasForeignKey(m => m.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
