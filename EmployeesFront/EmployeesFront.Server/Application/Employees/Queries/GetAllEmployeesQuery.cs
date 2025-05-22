using MediatR;
using EmployeesFront.Server.Domain;

namespace EmployeesFront.Server.Application.Employees.Queries
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<IdCandidates>>
    {        
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
    }
} 