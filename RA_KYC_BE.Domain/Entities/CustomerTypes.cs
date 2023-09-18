using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("CustomerTypes")]
    public class CustomerTypes : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
