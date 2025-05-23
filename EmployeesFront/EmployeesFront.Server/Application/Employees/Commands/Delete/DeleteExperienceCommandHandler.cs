using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeesFront.Server.Infrastructure;

namespace EmployeesFront.Server.Application.Employees.Commands.Delete
{
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteExperienceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = await _context.CandidateExperiences
                .FirstOrDefaultAsync(e => e.IdCandidateExperience == request.IdExperience, cancellationToken);

            if (experience == null)
            {
                return false;
            }

            _context.CandidateExperiences.Remove(experience);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
} 