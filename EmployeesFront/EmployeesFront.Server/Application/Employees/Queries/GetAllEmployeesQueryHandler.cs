using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;

namespace EmployeesFront.Server.Application.Employees.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<IdCandidates>>
    {
        private readonly AppDbContext _context;

        public GetAllEmployeesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IdCandidates>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Employees.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(e => 
                    e.Name.Contains(request.SearchTerm) ||
                    e.Email.Contains(request.SearchTerm) ||
                    e.Surname.Contains(request.SearchTerm)                    
                );
            }

            // Apply sorting if provided
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "name" => request.SortDescending 
                        ? query.OrderByDescending(e => e.Name)
                        : query.OrderBy(e => e.Name),
                    "email" => request.SortDescending
                        ? query.OrderByDescending(e => e.Email)
                        : query.OrderBy(e => e.Email),
                   "surName" => request.SortDescending
                        ? query.OrderByDescending(e=> e.Surname)
                        : query.OrderBy(e => e.Surname),
                    _ => query.OrderBy(e => e.IdCandidate)
                };
            }
            else
            {
                // Default sorting by Id
                query = query.OrderBy(e => e.IdCandidate);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
} 