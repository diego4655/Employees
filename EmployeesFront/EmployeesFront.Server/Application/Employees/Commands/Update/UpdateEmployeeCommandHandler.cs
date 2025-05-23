using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;
using System;
using EmployeesFront.Server.Application.Employees.Commands.Create;

namespace EmployeesFront.Server.Application.Employees.Commands.Update
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        private IdCandidates CheckEmployeeDifferences(IdCandidates employee, UpdateEmployeeCommand employeeCommand)
        {
            IdCandidates employeeUpdated = new IdCandidates();
            employeeUpdated.IdCandidate = employee.IdCandidate;
            employeeUpdated.Name = employeeCommand.Name ?? employee.Name;
            employeeUpdated.Surname = employeeCommand.Surname ?? employee.Surname;
            employeeUpdated.Birthdate = employeeCommand.Birthdate ?? employee.Birthdate;
            employeeUpdated.Email = employeeCommand.Email ?? employee.Email;
            employeeUpdated.ModifyDate = DateTime.UtcNow.AddHours(CreateEmployeeCommandHandler.timezone);

            return employeeUpdated;
        }

        private async Task<IdCandidates> FindEmployee(int idCandidate)
        {
            try
            {
                return await _context.Employees.FindAsync((int)idCandidate) ?? throw new NullReferenceException("No existe un empleado con ese codigo");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await FindEmployee(request.Id);
                IdCandidates employeeUpdated = CheckEmployeeDifferences(employee, request);
                await saveEmployee(employeeUpdated);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task saveEmployee(IdCandidates employee)
        {
            try
            {
                await _context.Employees
                    .Where(canditate => canditate.IdCandidate == employee.IdCandidate)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(e => e.Name, employee.Name)
                        .SetProperty(e => e.Surname, employee.Surname)
                        .SetProperty(e => e.Email, employee.Email)
                        .SetProperty(e => e.Birthdate, employee.Birthdate)
                        .SetProperty(e => e.InsertDate, employee.InsertDate)
                        .SetProperty(e => e.ModifyDate, employee.ModifyDate)
                    );

            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error al guardar el empleado: {ex.Message}");
            }
        }

    }
}