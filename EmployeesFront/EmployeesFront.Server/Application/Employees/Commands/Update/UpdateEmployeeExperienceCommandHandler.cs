using EmployeesFront.Server.Application.Employees.Commands.Create;
using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesFront.Server.Application.Employees.Commands.Update
{
    public class UpdateEmployeeExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, bool>
    {
        private readonly AppDbContext _context;
        public UpdateEmployeeExperienceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var experiences = await FindCandidateExperiences(request);
                CandidateExperiences experience =  CheckEmployeeExperienceDifferences(experiences, request) ;                
                await saveCandidateExperiences(experience);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al actualizar la informacion {ex.Message}");
            }

        }

        private async Task<CandidateExperiences> FindCandidateExperiences(UpdateExperienceCommand request)
        {
            try
            {
                if (request.IdExperience == 0)
                {
                    throw new ArgumentNullException($"La experiencia para el usuario no existe");
                }

                return await _context.CandidateExperiences
                    .Where(x => x.IdCandidateExperience == request.IdExperience)
                    .FirstOrDefaultAsync() ?? throw new ArgumentNullException($"No existe el registro a actualizar");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        private CandidateExperiences CheckEmployeeExperienceDifferences(CandidateExperiences experiences, UpdateExperienceCommand request)
        {
            try
            {
                experiences.Company = request.Company ?? experiences.Company;
                experiences.Job = request.Job ?? experiences.Job;
                experiences.Description = request.Description ?? experiences.Description;
                experiences.Salary = request.Salary ?? experiences.Salary;
                experiences.BeginDate = request.BeginDate ?? experiences.BeginDate;
                experiences.EndDate = request.EndDate ?? experiences.EndDate;
                experiences.ModifyDate = DateTime.UtcNow.AddHours(CreateEmployeeCommandHandler.timezone);

                return experiences;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"Error comparando la experiencia de los candidatos {ex.Message}");
            }
        }

        public async Task saveCandidateExperiences(CandidateExperiences candidateExperiences)
        {   
            try
            {
                _context.CandidateExperiences.Update(candidateExperiences);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al guardar las experiencias del empleado: {ex.Message}");
            }
        }
    }
}
