using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("MaritalStatuses")]
    public class MaritalStatus : BaseEntity
    {
        [Required]
        public string Status { get; set; }
    }
}
