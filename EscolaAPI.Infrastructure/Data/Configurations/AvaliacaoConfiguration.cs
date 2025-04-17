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
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {

            // Configuração tabela e chave primária
            builder.ToTable("Avaliacoes");
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Descricao).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Data).IsRequired();
            builder.Property(a => a.Peso).IsRequired().HasColumnType("decimal(5,2)");
            
            // Relacionamento N:1 com Turma
            builder.HasOne(a => a.Turma)
                  .WithMany(t => t.Avaliacoes)
                  .HasForeignKey(a => a.TurmaId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamento 1:N com Nota
            builder.HasMany(a => a.Notas)
                  .WithOne(n => n.Avaliacao)
                  .HasForeignKey(n => n.AvaliacaoId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
