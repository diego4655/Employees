using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeesFront.Server.Domain;
using EmployeesFront.Server.Infrastructure;
using Employees.Application.Employees.Commands;
using System;

namespace EmployeesFront.Server.Application.Employees.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        private async Task<IdCandidates> CheckEmployeeDifferences(IdCandidates employee, UpdateEmployeeCommand employeeCommand)
        {
            IdCandidates employeeUpdated = new IdCandidates();

            employeeUpdated.Name = employeeCommand.Name ?? employee.Name;
            employeeUpdated.Surname = employeeCommand.Surname ?? employee.Surname;
            employeeUpdated.Birthdate = employeeCommand.Birthdate ?? employee.Birthdate;
            employeeUpdated.Email = employeeCommand.Email ?? employee.Email;
            employeeUpdated.ModifyDate = DateTime.UtcNow.AddHours(CreateEmployeeCommandHandler.timezone);

            return employeeUpdated;
        }

        private async Task<IdCandidates> FindEmployee(int? idCandidate)
        {
            try
            {
                if (idCandidate == null)
                {
                    throw new ArgumentException("El empleado no existe");
                }

                return await _context.Employees.FindAsync((int)idCandidate);
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
                IdCandidates employeeUpdated = await CheckEmployeeDifferences(employee, request);                
                

                CreateEmployeeCommandHandler create = new CreateEmployeeCommandHandler(_context);
                await create.saveEmployee(employeeUpdated);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        

        
    }
} 