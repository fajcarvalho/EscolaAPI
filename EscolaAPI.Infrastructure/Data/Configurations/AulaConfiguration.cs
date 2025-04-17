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
    public class AulaConfiguration : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            // Configuração tabela e chave primária
            builder.ToTable("Aulas");
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Data).IsRequired();
            builder.Property(a => a.Conteudo).HasMaxLength(500);
            
            // Relacionamento N:1 com Turma
            builder.HasOne(a => a.Turma)
                   .WithMany(t => t.Aulas)
                   .HasForeignKey(a => a.TurmaId)
                   .OnDelete(DeleteBehavior.Cascade);
            
                // Relacionamento 1:N com Frequencia
            builder.HasMany(a => a.Frequencias)
                   .WithOne(f => f.Aula)
                   .HasForeignKey(f => f.AulaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
