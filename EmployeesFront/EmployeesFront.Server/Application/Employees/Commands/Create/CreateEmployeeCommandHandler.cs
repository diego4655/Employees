using MediatR;
using EmployeesFront.Server.Infrastructure;
using EmployeesFront.Server.Domain;
using Azure.Core;

namespace EmployeesFront.Server.Application.Employees.Commands.Create
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly AppDbContext _context;
        public const int timezone = -5;

        public CreateEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
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
                return true;
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
    }
}