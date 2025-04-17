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
    public class NotaConfiguration : IEntityTypeConfiguration<Nota>
    {
        public void Configure(EntityTypeBuilder<Nota> builder)
        {
            builder.ToTable("Notas");
            builder.HasKey(n => n.Id);
            
            builder.Property(n => n.Valor).HasColumnType("decimal(4,2)");
            builder.Property(n => n.Observacao).HasMaxLength(200);
            
            // Relacionamento N:1 com Matricula
            builder.HasOne(n => n.Matricula)
                  .WithMany(m => m.Notas)
                  .HasForeignKey(n => n.MatriculaId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Relacionamento N:1 com Avaliacao
            builder.HasOne(n => n.Avaliacao)
                  .WithMany(a => a.Notas)
                  .HasForeignKey(n => n.AvaliacaoId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
