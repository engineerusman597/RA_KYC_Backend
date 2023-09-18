using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.EducationLevel
{
    public class EducationLevelDto
    {
        public int Id { get; set; }
        [Required]
        public string Level { get; set; }
        public bool IsActive { get; set; }
    }
}
