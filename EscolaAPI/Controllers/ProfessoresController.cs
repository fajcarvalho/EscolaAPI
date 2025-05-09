using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Professores;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Professores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessoresController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProfessoresController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProfessorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetAll()
        {
            var query = new GetAllProfessoresQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProfessorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfessorDto>> GetById(int id)
        {
            var query = new GetProfessorByIdQuery(id);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateProfessorDto createProfessorDto)
        {
            try
            {
                var command = new CreateProfessorCommand(createProfessorDto);
                var result = await _mediator.Send(command);
        
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // Erro de chave estrangeira
                return BadRequest("Erro de referência: O departamento informado não existe no sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar professor: {ex.Message}");
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateProfessorDto updateProfessorDto)
        {
            try
            {
                var command = new UpdateProfessorCommand(updateProfessorDto);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound();
                    
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar professor: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                var command = new DeleteProfessorCommand(id);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound();
                    
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir professor: {ex.Message}");
            }
        }
    }
}