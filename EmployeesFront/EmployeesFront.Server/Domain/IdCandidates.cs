using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EmployeesFront.Server.Domain
{
    public class IdCandidates
    {
        [Key]
        public int IdCandidate { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
} 