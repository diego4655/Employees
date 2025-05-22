using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesFront.Server.Application.Employees.Queries
{
    public class GetAllExperiencesQueryHandler : IRequestHandler<GetAllExperiencesQuery, List<CandidateExperiences>>
    {
        private readonly AppDbContext _context;
        public GetAllExperiencesQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CandidateExperiences>> Handle(GetAllExperiencesQuery request, CancellationToken cancellationToken)
        {
          return await _context.CandidateExperiences.Where(data=> data.IdCandidate == request.EmployeeId).ToListAsync();    
        }
    }
}
