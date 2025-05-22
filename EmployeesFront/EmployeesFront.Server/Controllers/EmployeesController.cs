using Microsoft.AspNetCore.Mvc;
using MediatR;
using Employees.Application.Employees.Queries;
using EmployeesFront.Server.Application.Employees.Commands.Create;
using EmployeesFront.Server.Application.Employees.Commands.Update;
using EmployeesFront.Server.Application.Employees.Queries;

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


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var query = new GetEmployeeQuery { Id = id };
            var employee = await _mediator.Send(query);
            return Ok(employee);
        }

        [HttpGet("{EmployeeId}/experiences")]
        public async Task<IActionResult> GetAllExperienceForEmployee(int EmployeeId)
        {
            var query = new GetAllExperiencesQuery { EmployeeId = EmployeeId };
            var experiences = await _mediator.Send(query);
            return Ok(experiences);
        }


       
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployee), new { id = employeeId }, employeeId);            
        }

        
        [HttpPost("{id}/experiences")]
        public async Task<IActionResult> CreateExperienceToEmployee(int id, CreateExperienceCommand experienceCommand)
        {
            experienceCommand.EmployeeId = id;
            var experienceToEmployee = await _mediator.Send(experienceCommand);
            return CreatedAtAction(nameof(GetEmployee), new { id }, experienceToEmployee);            
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if(id == 0)
            { 
                return BadRequest(new { error = "ID de empleado invalido" }); 
            }

            command.Id = id;
            var result = await _mediator.Send(command);            
            if (!result)
            {
                return NotFound(new { error = $"Empleado con el Id {id} no encontrado" });
            }               
            return Ok();
        }

        [HttpPatch("{id}/experiences")]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] UpdateExperienceCommand command)
        {
            if (id == 0)
            {
                return BadRequest(new { error = "ID de empleado invalido" });
            }
            command.IdExperience = id;
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(new { error = $"Empleado con el Id {id} no encontrado" });
            }
            return Ok();

        }
        // DELETE /api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            // TODO: Implement delete employee
            return NoContent();
        }
    }
} 