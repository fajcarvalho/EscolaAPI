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
    public class ItemHistoricoConfiguration : IEntityTypeConfiguration<ItemHistorico>
    {
        public void Configure(EntityTypeBuilder<ItemHistorico> builder)
        {
            builder.ToTable("ItensHistorico");
            builder.HasKey(ih => ih.Id);
            
            builder.Property(ih => ih.Semestre).IsRequired().HasMaxLength(10);
            builder.Property(ih => ih.Ano).IsRequired();
            builder.Property(ih => ih.NotaFinal).HasColumnType("decimal(4,2)");
            builder.Property(ih => ih.Observacao).HasMaxLength(200);
            
            // Relacionamento N:1 com Historico
            builder.HasOne(ih => ih.Historico)
                  .WithMany(h => h.Itens)
                  .HasForeignKey(ih => ih.HistoricoId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamento N:1 com Disciplina
            builder.HasOne(ih => ih.Disciplina)
                  .WithMany()  // Não há navegação de volta na Disciplina
                  .HasForeignKey(ih => ih.DisciplinaId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
