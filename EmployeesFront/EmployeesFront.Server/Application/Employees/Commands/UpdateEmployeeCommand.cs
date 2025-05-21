using Employees.Application.Employees.Commands;
using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands
{
    public class UpdateEmployeeCommand: CreateEmployeeCommand, IRequest<bool>
    {
        public int? Id { get; set; }
    }
}
