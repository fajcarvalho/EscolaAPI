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
    public class DisciplinaPreRequisitoConfiguration : IEntityTypeConfiguration<DisciplinaPreRequisito> 
    {
        public void Configure(EntityTypeBuilder<DisciplinaPreRequisito> builder) 
        {
            // Configuração de tabela
            builder.ToTable("DisciplinasPreRequisitos");
        
            // Chave composta
            builder.HasKey(dp => new { dp.DisciplinaId, dp.PreRequisitoId });
        
            // Relacionamentos
            builder.HasOne(dp => dp.Disciplina)
                   .WithMany(d => d.PreRequisitos)
                   .HasForeignKey(dp => dp.DisciplinaId)
                   .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata circular
        
            builder.HasOne(dp => dp.PreRequisito)
                   .WithMany(d => d.DisciplinasQuePreRequisitamEsta)
                   .HasForeignKey(dp => dp.PreRequisitoId)
                   .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata circular
        }

    }
}
