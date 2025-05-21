using MediatR;
using EmployeesFront.Server.Infrastructure;
using EmployeesFront.Server.Domain;
using Azure.Core;

namespace Employees.Application.Employees.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly AppDbContext _context;
        public const int timezone = -5;

        public CreateEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> createEmployeeExperiences(CreateEmployeeCommand employeeCommand, int idCandidate)
        {
            List<CandidateExperiences> candidateExperiences = new List<CandidateExperiences>();
            employeeCommand.Experiences.ForEach(exp =>
            {

                candidateExperiences.Add(new CandidateExperiences()
                {
                    IdCandidate = idCandidate,
                    Company = exp.Company,
                    Job = exp.Job,
                    Description = exp.Description,
                    Salary = exp.Salary,
                    BeginDate = exp.BeginDate,
                    EndDate = exp.EndDate,
                });
            });

            await saveCandidateExperiences(candidateExperiences);
            return true;
        }
      

        public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = new IdCandidates
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Birthdate = (DateTime)request.Birthdate,
                    Email = request.Email,
                    InsertDate = DateTime.UtcNow.AddHours(timezone),
                };
                await saveEmployee(employee);
                return await createEmployeeExperiences(request, employee.IdCandidate);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al crear el empleado: {ex.Message}");
            }
        }

        public async Task saveEmployee(IdCandidates employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al guardar el empleado: {ex.Message}");
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