using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Application.Mappings;
using EscolaAPI.Application.Queries.Alunos;
using EscolaAPI.Domain.Entities;
using EscolaAPI.Domain.Enums;
using Moq;
using Xunit;
namespace EscolaAPI.Tests.Queries 
{
    public class GetAlunoByIdQueryHandlerTests 
    {
        private readonly Mock<IAlunoRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly GetAlunoByIdQueryHandler _handler;
        
        public GetAlunoByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IAlunoRepository>();

            // Configurar AutoMapper com o perfil real
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetAlunoByIdQueryHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ExistingId_ShouldReturnAlunoDto()
        {
            // Arrange
            var alunoId = 1;
            var query = new GetAlunoByIdQuery(alunoId);

            var aluno = new Aluno
            { 
                Id = alunoId,
                Nome = "Teste",
                Matricula = "20250001",
                DataNascimento = new DateTime(2000, 1, 1),
                Email = "teste@email.com",
                Telefone = "999999999",
                DataIngresso = DateTime.Now,
                Status = StatusAluno.Ativo,
                Endereco = new Endereco
                {
                    Id = 1,
                    Rua = "Rua Teste",
                    Numero = "123",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Estado = "MG",
                    CEP = "30000-000",
                    AlunoId = alunoId
                }        
            };

            _mockRepository.Setup(repo => repo.GetAlunoWithEnderecoAsync(alunoId))
                .ReturnsAsync(aluno);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(alunoId, result.Id);
            Assert.Equal("Teste", result.Nome);
            Assert.Equal("20250001", result.Matricula);

            // Verifica se o endereço foi mapeado corretamente
            Assert.NotNull(result.Endereco);
            Assert.Equal("Rua Teste", result.Endereco.Rua);
            Assert.Equal("MG", result.Endereco.Estado);
        }

        [Fact]
        public async Task Handle_NonExistingId_ShouldReturnNull() 
        {
            // Arrange
            var alunoId = 999; // ID que não existe
            var query = new GetAlunoByIdQuery(alunoId);

            _mockRepository.Setup(repo => repo.GetAlunoWithEnderecoAsync(alunoId))
                .ReturnsAsync((Aluno)null); // Simula que o aluno não foi encontrado

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
