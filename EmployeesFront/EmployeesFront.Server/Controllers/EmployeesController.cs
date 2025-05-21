using Microsoft.AspNetCore.Mvc;
using MediatR;
using Employees.Application.Employees.Commands;
using Employees.Application.Employees.Queries;
using EmployeesFront.Server.Application.Employees.Commands;

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return Ok(employeeId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var query = new GetEmployeeQuery { Id = id };
            var employee = await _mediator.Send(query);
            return Ok(employee);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEmployee([FromQuery]int id, [FromBody] UpdateEmployeeCommand command)
        {
           if(id == 0){ return BadRequest(); }

           command.Id = id;
            var result = await _mediator.Send(command);            
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 