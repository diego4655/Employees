using Microsoft.AspNetCore.Mvc;
using MediatR;
using Employees.Application.Employees.Queries;
using EmployeesFront.Server.Application.Employees.Commands.Create;
using EmployeesFront.Server.Application.Employees.Commands.Update;
using EmployeesFront.Server.Application.Employees.Queries;
using EmployeesFront.Server.Application.Employees.Commands.Delete;

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


        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var query = new GetEmployeeQuery { Id = employeeId };
            var employee = await _mediator.Send(query);
            return Ok(employee);
        }

        [HttpGet("{employeeId}/experiences")]
        public async Task<IActionResult> GetAllExperienceForEmployee(int employeeId)
        {
            var query = new GetAllExperiencesQuery { EmployeeId = employeeId };
            var experiences = await _mediator.Send(query);
            return Ok(experiences);
        }


       
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            if (!employeeId)
            {
                return BadRequest();    
            }
            return Ok(employeeId);            
        }

        
        [HttpPost("{experienceId}/experiences")]
        public async Task<IActionResult> CreateExperienceToEmployee(int experienceId, CreateExperienceCommand experienceCommand)
        {
            experienceCommand.EmployeeId = experienceId;
            var experienceToEmployee = await _mediator.Send(experienceCommand);
            if (!experienceToEmployee)
            {
                return BadRequest();    
            }
            return Ok(experienceToEmployee);            
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

        [HttpPatch("{experienceId}/experiences")]
        public async Task<IActionResult> UpdateExperience(int experienceId, [FromBody] UpdateExperienceCommand command)
        {
            if (experienceId == 0)
            {
                return BadRequest(new { error = "ID de empleado invalido" });
            }
            command.IdExperience = experienceId;
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(new { error = $"Empleado con el Id {experienceId} no encontrado" });
            }
            return Ok();

        }
        
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            DeleteEmployeeCommand  deleteEmployee = new DeleteEmployeeCommand() { IdCandidate = employeeId};
            var employee = await _mediator.Send(deleteEmployee);
            return Ok(employee);
        }

        [HttpDelete("{experienceId}/experiences")]
        public async Task<IActionResult> DeleteExperience(int experienceId)
        {
            DeleteExperienceCommand deleteExperience = new DeleteExperienceCommand() { IdExperience = experienceId };
            var experience = await _mediator.Send(deleteExperience);
            return Ok(experience);

        }
    }
} 