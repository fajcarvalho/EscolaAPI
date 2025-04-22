using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Alunos;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Alunos;
using EscolaAPI.Controllers;
using EscolaAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EscolaAPI.Tests.Controllers
{
    public class AlunosControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly AlunosController _controller;
        
        public AlunosControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new AlunosController(_mockMediator.Object);
        }
        
        [Fact]
        public async Task GetAll_ShouldReturnAllAlunos()
        {
            // Arrange
            var alunos = new List<AlunoDto>
            {
                new AlunoDto { Id = 1, Nome = "Aluno 1", Matricula = "20250001" },
                new AlunoDto { Id = 2, Nome = "Aluno 2", Matricula = "20250002" }
            };
            
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllAlunosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(alunos);
                
            // Act
            var result = await _controller.GetAll();
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<AlunoDto>>(okResult.Value);
            Assert.Equal(2, ((List<AlunoDto>)returnValue).Count);
        }
        
        [Fact]
        public async Task GetById_ExistingId_ShouldReturnAluno()
        {
            // Arrange
            var alunoId = 1;
            var alunoDto = new AlunoDto 
            { 
                Id = alunoId, 
                Nome = "Teste", 
                Matricula = "20250001",
                Email = "teste@email.com"
            };
            
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAlunoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(alunoDto);
                
            // Act
            var result = await _controller.GetById(alunoId);
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AlunoDto>(okResult.Value);
            Assert.Equal(alunoId, returnValue.Id);
            Assert.Equal("Teste", returnValue.Nome);
        }
        
        [Fact]
        public async Task GetById_NonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var alunoId = 999;
            
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAlunoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AlunoDto)null);
                
            // Act
            var result = await _controller.GetById(alunoId);
            
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public async Task Create_ValidAluno_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateAlunoDto
            {
                Nome = "Novo Aluno",
                Matricula = "20250003",
                DataNascimento = new DateTime(2000, 1, 1),
                Status = StatusAluno.Ativo
            };
            
            var alunoId = 1;
            
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateAlunoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(alunoId);
                
            // Act
            var result = await _controller.Create(createDto);
            
            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(AlunosController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(alunoId, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(alunoId, createdAtActionResult.Value);
        }
        
        [Fact]
        public async Task Update_ExistingAluno_ShouldReturnNoContent()
        {
            // Arrange
            var updateDto = new UpdateAlunoDto
            {
                Id = 1,
                Nome = "Aluno Atualizado",
                Status = StatusAluno.Ativo
            };
            
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateAlunoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
                
            // Act
            var result = await _controller.Update(updateDto);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async Task Update_NonExistingAluno_ShouldReturnNotFound()
        {
            // Arrange
            var updateDto = new UpdateAlunoDto
            {
                Id = 999,
                Nome = "Aluno Inexistente"
            };
            
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateAlunoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
                
            // Act
            var result = await _controller.Update(updateDto);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public async Task Delete_ExistingId_ShouldReturnNoContent()
        {
            // Arrange
            var alunoId = 1;
            
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteAlunoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
                
            // Act
            var result = await _controller.Delete(alunoId);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async Task Delete_NonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var alunoId = 999;
            
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteAlunoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
                
            // Act
            var result = await _controller.Delete(alunoId);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}