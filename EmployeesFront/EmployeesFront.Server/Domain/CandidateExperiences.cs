using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesFront.Server.Domain
{
    public class CandidateExperiences
    {
        [Key]
        public int IdCandidateExperience { get; set; }
        [ForeignKey("IdCandidate")]
        public int IdCandidate{ get; set; }
        [StringLength(100) ]
        public string Company { get; set; }
        [StringLength(100)]
        public string Job { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        
        public int Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
