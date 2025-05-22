using EmployeesFront.Server.Application.Employees.Commands.Create;
using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Update
{
    public class UpdateExperienceCommand : CreateExperienceCommand, IRequest<bool>
    {
        public int IdExperience { get; set; }
    }
}
