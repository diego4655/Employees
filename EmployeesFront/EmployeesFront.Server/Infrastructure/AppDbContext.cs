using Microsoft.EntityFrameworkCore;
using EmployeesFront.Server.Domain;

namespace EmployeesFront.Server.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<IdCandidates> Employees { get; set; }
        public DbSet<CandidateExperiences> CandidateExperiences { get; set; }
    }
} 