using EmployeesFront.Server.Application.Employees.Commands.Create;
using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Update
{
    public class UpdateEmployeeCommand : CreateEmployeeCommand, IRequest<bool>
    {
        public int Id { get; set; }
    }
}
