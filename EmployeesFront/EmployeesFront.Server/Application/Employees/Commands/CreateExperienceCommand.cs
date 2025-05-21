using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands
{
    public class CreateExperienceCommand:IRequest<bool>
    {
        public int IdCandidate { get; set; }
        public int IdExperience { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
