using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("EducationLevels")]
    public class EducationLevel : BaseEntity
    {
        [Required]
        public string Level { get; set; }
    }
}
