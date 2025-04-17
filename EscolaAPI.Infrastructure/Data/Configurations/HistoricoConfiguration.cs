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
    public class HistoricoConfiguration : IEntityTypeConfiguration<Historico>
    {
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            builder.ToTable("Historicos");
            builder.HasKey(h => h.Id);
            
            builder.Property(h => h.DataGeracao).IsRequired();
            
            // Relacionamento 1:1 com Aluno
            builder.HasOne(h => h.Aluno)
                  .WithOne(a => a.Historico)
                  .HasForeignKey<Historico>(h => h.AlunoId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamento 1:N com ItemHistorico
            builder.HasMany(h => h.Itens)
                  .WithOne(i => i.Historico)
                  .HasForeignKey(i => i.HistoricoId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}