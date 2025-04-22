using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscolaAPI.Application.Commands.Alunos;
using EscolaAPI.Application.DTOs;
using EscolaAPI.Application.Queries.Alunos;
using MediatR;

namespace EscolaAPI.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase 
    {
        private readonly IMediator _mediator;

        public AlunosController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlunoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAll()
        {
            var query = new GetAllAlunosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AlunoDto>> GetById(int id) 
        {
            var query = new GetAlunoByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("matricula/{matricula}")]
        [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AlunoDto>> GetByMatricula(string matricula) {
            var query = new GetAlunoByMatriculaQuery(matricula);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateAlunoDto createAlunoDto)
        { 
            var command = new CreateAlunoCommand(createAlunoDto);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateAlunoDto updateAlunoDto)
        {
            var command = new UpdateAlunoCommand(updateAlunoDto);
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) 
        {
            var command = new DeleteAlunoCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
