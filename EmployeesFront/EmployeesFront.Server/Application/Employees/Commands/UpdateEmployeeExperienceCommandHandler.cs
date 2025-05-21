using Employees.Application.Employees.Commands;
using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesFront.Server.Application.Employees.Commands
{
    public class UpdateEmployeeExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, bool>
    {
        private readonly AppDbContext _context;
        public UpdateEmployeeExperienceCommandHandler(AppDbContext context)
        {
                _context = context;
        }

        public async Task<bool> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var experiences = await FindCandidateExperiences(request);
                List<CandidateExperiences> experiencesList = new List<CandidateExperiences> { await CheckEmployeeExperienceDifferences(experiences, request) };
                CreateEmployeeCommandHandler creater = new CreateEmployeeCommandHandler(_context);
                await creater.saveCandidateExperiences(experiencesList);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al actualizar la informacion {ex.Message}");
            }
            
        }

        private async Task<CandidateExperiences> FindCandidateExperiences(CreateExperienceCommand request)
        {
            try
            {
                if (request.IdCandidate == null || request.IdExperience == null)
                {
                    throw new ArgumentNullException($"El empleado para buscar experiencias no existe");
                }

                return await _context.CandidateExperiences
                    .Where(x => x.IdCandidate == request.IdCandidate && x.IdCandidateExperience == request.IdExperience)
                    .FirstOrDefaultAsync() ?? throw new ArgumentNullException($"No existe el registro a actualizar");


            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }

        private async Task<CandidateExperiences> CheckEmployeeExperienceDifferences(CandidateExperiences experiences, CreateExperienceCommand request)
        {
            try
            {
                    experiences.Company = request.Company ?? experiences.Company;
                    experiences.Job = request.Job ?? experiences.Job;
                    experiences.Description = request.Description ?? experiences.Description;
                    experiences.Salary = request.Salary;
                    experiences.BeginDate = request.BeginDate;
                    experiences.EndDate = request.EndDate ?? experiences.EndDate;
                    experiences.ModifyDate = DateTime.UtcNow.AddHours(CreateEmployeeCommandHandler.timezone);                
                
                return experiences;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Error comparando la experiencia de los candidatos {ex.Message}");
            }
        }
    }
}
