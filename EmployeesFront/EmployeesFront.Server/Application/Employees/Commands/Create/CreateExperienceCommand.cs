using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Create
{
    public class CreateExperienceCommand : IRequest<bool>
    {
        public int? EmployeeId { get; set; }        
        public string? Company { get; set; }
        public string? Job { get; set; }
        public string? Description { get; set; }
        public int? Salary { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
