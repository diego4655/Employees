using MediatR;
using EmployeesFront.Server.Domain;

namespace Employees.Application.Employees.Queries
{
    public class GetEmployeeQuery : IRequest<IdCandidates>
    {
        public int Id { get; set; }
    }
} 