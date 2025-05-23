using EmployeesFront.Server.Domain;
using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Create
{
    public class CreateEmployeeCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }        
    }

}