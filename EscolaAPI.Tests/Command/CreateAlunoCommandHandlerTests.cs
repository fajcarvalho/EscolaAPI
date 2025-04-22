using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EscolaAPI.Application.Commands.Alunos;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Interfaces;
using EscolaAPI.Application.Mappings;
using EscolaAPI.Domain.Entities;
using EscolaAPI.Domain.Enums;
using Moq;
using Xunit;

namespace EscolaAPI.Tests.Command 
{
    public class CreateAlunoCommandHandlerTests 
    {
        private readonly Mock<IAlunoRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly CreateAlunoCommandHandler _handler;

        public CreateAlunoCommandHandlerTests()
        {
            _mockRepository = new Mock<IAlunoRepository>();

            // Configurar AutoMapper com o perfil real
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateAlunoCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateAluno()
        {
            // Arrange
            var createDto = new CreateAlunoDto
            {
                Nome = "Teste",
                Matricula = "20250001",
                DataNascimento = new DateTime(2000, 1, 1),
                Email = "teste@email.com",
                Telefone = "123456789",
                DataIngresso = DateTime.Now,
                Status = StatusAluno.Ativo,
                Endereco = new CreateEnderecoDto {
                    Rua = "Rua Teste",
                    Numero = "123",
                    Complemento = "Apto 1",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Estado = "MG",
                    CEP = "30000-000"
                }
            };

            var command = new CreateAlunoCommand(createDto);

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Aluno>()))
                .Returns(Task.CompletedTask)
                .Callback<Aluno>(aluno => 
                {
                    aluno.Id = 1; // Simular o ID gerado pelo banco de dados
                });

            _mockRepository.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result); // Verifica se o ID retornado é o esperado
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Aluno>()), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
