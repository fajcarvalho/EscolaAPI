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
    public class EspecializacaoConfiguration : IEntityTypeConfiguration<Especializacao>
    {
        public void Configure(EntityTypeBuilder<Especializacao> builder)
        {
            builder.ToTable("Especializacoes");
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Area).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Descricao).HasMaxLength(500);
            
            // Relacionamento N:1 com Professor
            builder.HasOne(e => e.Professor)
                   .WithMany(p => p.Especializacoes)
                   .HasForeignKey(e => e.ProfessorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
