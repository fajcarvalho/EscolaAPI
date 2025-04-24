using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Departamentos;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Departamentos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public DepartamentosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartamentoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DepartamentoDto>>> GetAll()
        {
            var query = new GetAllDepartamentosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DepartamentoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartamentoDto>> GetById(int id)
        {
            var query = new GetDepartamentoByIdQuery(id);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateDepartamentoDto createDepartamentoDto)
        {
            try
            {
                var command = new CreateDepartamentoCommand(createDepartamentoDto);
                var result = await _mediator.Send(command);
                
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // Erro de chave estrangeira
                return BadRequest("Erro de referência: O coordenador informado não existe no sistema.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar departamento: {ex.Message}");
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateDepartamentoDto updateDepartamentoDto)
        {
            try
            {
                var command = new UpdateDepartamentoCommand(updateDepartamentoDto);
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
                return BadRequest($"Erro ao atualizar departamento: {ex.Message}");
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
                var command = new DeleteDepartamentoCommand(id);
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
                return BadRequest($"Erro ao excluir departamento: {ex.Message}");
            }
        }
    }
}