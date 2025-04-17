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
    public class FrequenciaConfiguration : IEntityTypeConfiguration<Frequencia>
    {
        public void Configure(EntityTypeBuilder<Frequencia> builder)
        {
            builder.ToTable("Frequencias");
            builder.HasKey(f => f.Id);
            
            builder.Property(f => f.Presente).IsRequired();
            builder.Property(f => f.Observacao).HasMaxLength(200);
            
            // Relacionamento N:1 com Matricula
            builder.HasOne(f => f.Matricula)
                   .WithMany(m => m.Frequencias)
                   .HasForeignKey(f => f.MatriculaId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            // Relacionamento N:1 com Aula
            builder.HasOne(f => f.Aula)
                   .WithMany(a => a.Frequencias)
                   .HasForeignKey(f => f.AulaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}