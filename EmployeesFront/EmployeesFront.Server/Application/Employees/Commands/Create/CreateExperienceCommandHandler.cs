using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;
using MediatR;

namespace EmployeesFront.Server.Application.Employees.Commands.Create
{
    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, bool>
    {
        private readonly AppDbContext _context;
        public CreateExperienceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.EmployeeId == 0)
                {
                    throw new ArgumentNullException($"El id del candidato es obligatorio");
                }

                var experience = new CandidateExperiences()
                {
                    IdCandidate = request.EmployeeId ?? throw new ArgumentNullException("El id del empleado es obligatorio"),
                    BeginDate = DateTime.UtcNow,
                    Company = request.Company,
                    Description = request.Description,
                    EndDate = DateTime.UtcNow,
                    InsertDate = DateTime.UtcNow,
                    Job = request.Job,
                    Salary = request.Salary ?? throw new ArgumentNullException("El salario del empleado es obligatorio"),
                };

                await saveCandidateExperiences(new List<CandidateExperiences>() { experience });
                return true;

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            
        }

        public async Task saveCandidateExperiences(List<CandidateExperiences> candidateExperiences)
        {
            try
            {
                _context.CandidateExperiences.AddRange(candidateExperiences);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al guardar las experiencias del empleado: {ex.Message}");
            }
        }
    }
}
