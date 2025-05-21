using MediatR;
using EmployeesFront.Server.Infrastructure;
using EmployeesFront.Server.Domain;

namespace Employees.Application.Employees.Queries
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, IdCandidates>
    {
        private readonly AppDbContext _context;

        public GetEmployeeQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IdCandidates> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.FindAsync(request.Id);
        }
    }
} 