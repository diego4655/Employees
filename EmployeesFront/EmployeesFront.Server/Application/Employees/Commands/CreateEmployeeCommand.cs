using EmployeesFront.Server.Application.Employees.Commands;
using MediatR;

namespace Employees.Application.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<bool>
    {        
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Email { get; set; }
        public List<CreateExperienceCommand>? Experiences { get; set; }
    }
    
} 