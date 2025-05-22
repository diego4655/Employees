using EmployeesFront.Server.Domain;
using MediatR;

namespace EmployeesFront.Server.Application.Employees.Queries
{
    public class GetAllExperiencesQuery:IRequest<List<CandidateExperiences>>
    {
        public int EmployeeId { get; set; }
    }
}
