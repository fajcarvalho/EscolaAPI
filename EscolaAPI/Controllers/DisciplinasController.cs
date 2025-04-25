using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Disciplinas;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Disciplinas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinasController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public DisciplinasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DisciplinaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DisciplinaDto>>> GetAll()
        {
            var query = new GetAllDisciplinasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DisciplinaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DisciplinaDto>> GetById(int id)
        {
            var query = new GetDisciplinaByIdQuery(id);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }
        
        [HttpGet("curso/{cursoId}")]
        [ProducesResponseType(typeof(IEnumerable<DisciplinaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DisciplinaDto>>> GetByCurso(int cursoId)
        {
            var query = new GetDisciplinasByCursoQuery(cursoId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateDisciplinaDto createDisciplinaDto)
        {
            try
            {
                var command = new CreateDisciplinaCommand(createDisciplinaDto);
                var result = await _mediator.Send(command);
                
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // Erro de chave estrangeira
                return BadRequest("Erro de referência: Uma das referências (curso, professor ou pré-requisito) não existe no sistema.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar disciplina: {ex.Message}");
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateDisciplinaDto updateDisciplinaDto)
        {
            try
            {
                var command = new UpdateDisciplinaCommand(updateDisciplinaDto);
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
                return BadRequest($"Erro ao atualizar disciplina: {ex.Message}");
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
                var command = new DeleteDisciplinaCommand(id);
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
                return BadRequest($"Erro ao excluir disciplina: {ex.Message}");
            }
        }
    }
}