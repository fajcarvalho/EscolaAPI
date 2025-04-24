using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Cursos;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Cursos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CursosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CursoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetAll()
        {
            var query = new GetAllCursosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CursoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CursoDto>> GetById(int id)
        {
            var query = new GetCursoByIdQuery(id);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCursoDto createCursoDto)
        {
            try
            {
                var command = new CreateCursoCommand(createCursoDto);
                var result = await _mediator.Send(command);
                
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // Erro de chave estrangeira
                return BadRequest("Erro de referência: O departamento informado não existe no sistema.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar curso: {ex.Message}");
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateCursoDto updateCursoDto)
        {
            try
            {
                var command = new UpdateCursoCommand(updateCursoDto);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound();
                    
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar curso: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteCursoCommand(id);
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound();
                    
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir curso: {ex.Message}");
            }
        }
    }
}