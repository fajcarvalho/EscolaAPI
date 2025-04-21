using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Infrastructure.Data;
using EscolaAPI.Domain.Entities;

namespace EscolaAPI.IntegrationTests 
{
    public class DatabaseCreationTests 
    {
        [Fact]
        public async Task Should_Create_Database_With_All_Tables()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                // Assert - Verificar se os DbSets estão acessíveis
                Assert.NotNull(context.Alunos);
                Assert.NotNull(context.Professores);
                Assert.NotNull(context.Departamentos);
                Assert.NotNull(context.Cursos);
                Assert.NotNull(context.Disciplinas);
                Assert.NotNull(context.Turmas);
                Assert.NotNull(context.Matriculas);
                Assert.NotNull(context.Avaliacoes);
                Assert.NotNull(context.Notas);
                Assert.NotNull(context.Aulas);
                Assert.NotNull(context.Frequencias);
                Assert.NotNull(context.Enderecos);
                Assert.NotNull(context.Especializacoes);
                Assert.NotNull(context.Historicos);
                Assert.NotNull(context.ItensHistorico);
            }
        }

        [Fact]
        public async Task Should_Create_SqlServer_Database()
        {
            // Arrange
            var connectionString = "Server=localhost;Database=EscolaDbTest;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                // Garantir que o banco de dados é recriado do zero
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                // Assert - Verificar se conseguimos adicionar um aluno
                var aluno = new Aluno
                {
                    Nome = "Teste",
                    Matricula = "20240001",
                    Email = "teste@email.com",
                    DataNascimento = new DateTime(2000, 1, 1),
                    DataIngresso = DateTime.Now,
                    Status = EscolaAPI.Domain.Enums.StatusAluno.Ativo
                };

                context.Alunos.Add(aluno);
                await context.SaveChangesAsync();

                var alunoSalvo = await context.Alunos.FirstOrDefaultAsync(a => a.Matricula == "20240001");
                Assert.NotNull(alunoSalvo);
                Assert.Equal("Teste", alunoSalvo.Nome);
            }
        }

    }
}
