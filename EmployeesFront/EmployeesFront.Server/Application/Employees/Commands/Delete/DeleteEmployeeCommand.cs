using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int IdCandidate { get; set; }
    }
} 