using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Delete
{
    public class DeleteExperienceCommand : IRequest<bool>
    {
        public int IdExperience { get; set; }
    }
} 