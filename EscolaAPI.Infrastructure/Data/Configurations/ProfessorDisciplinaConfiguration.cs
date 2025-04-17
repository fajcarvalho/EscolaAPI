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
    public class ProfessorDisciplinaConfiguration : IEntityTypeConfiguration<ProfessorDisciplina> 
    {
        public void Configure(EntityTypeBuilder<ProfessorDisciplina> builder) 
        {
            // Configuração de tabela
            builder.ToTable("ProfessorDisciplinas");

            // Chave composta
            builder.HasKey(pd => new { pd.ProfessorId, pd.DisciplinaId });

            // Relacionamentos
            builder.HasOne(pd => pd.Professor)
               .WithMany(p => p.Disciplinas)
               .HasForeignKey(pd => pd.ProfessorId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pd => pd.Disciplina)
                .WithMany(d => d.Professores)
                .HasForeignKey(pd => pd.DisciplinaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
