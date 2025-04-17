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
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Rua).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Numero).IsRequired().HasMaxLength(10);
            builder.Property(e => e.Complemento).HasMaxLength(50);
            builder.Property(e => e.Bairro).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Cidade).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Estado).IsRequired().HasMaxLength(2);
            builder.Property(e => e.CEP).IsRequired().HasMaxLength(10);
            
            // Relacionamento 1:1 com Aluno
            builder.HasOne(e => e.Aluno)
                   .WithOne(a => a.Endereco)
                   .HasForeignKey<Endereco>(e => e.AlunoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}