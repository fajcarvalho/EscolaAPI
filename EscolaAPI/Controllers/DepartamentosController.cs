using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Departamentos;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Departamentos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateDepartamentoDto createDepartamentoDto)
        {
            var command = new CreateDepartamentoCommand(createDepartamentoDto);
            var result = await _mediator.Send(command);
            
            return CreatedAtAction(nameof(GetAll), new { id = result }, result);
        }
    }
}