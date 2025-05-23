using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeesFront.Server.Infrastructure;
using EmployeesFront.Server.Domain;

namespace EmployeesFront.Server.Application.Employees.Commands.Delete
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.IdCandidate == request.IdCandidate, cancellationToken);
            List<CandidateExperiences> candidateExperiences = await _context.CandidateExperiences.Where(exp => exp.IdCandidate == request.IdCandidate).ToListAsync();

            if (employee == null)
            {
                return false;
            }

            if (candidateExperiences.Any()) 
            { 
                _context.CandidateExperiences.RemoveRange(candidateExperiences);
                await _context.SaveChangesAsync(cancellationToken);
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
} 