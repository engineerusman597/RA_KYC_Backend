using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    public class OFACControl : BaseEntity
    {
        public string ParentCode { get; set; }
        public string Category { get; set; }
        public string ControlCode { get; set; }
        public string Strong3 { get; set; }
        public string Adequate2 { get; set; }
        public string Weak1 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Score { get; set; }
        public string Comments { get; set; }
        public string Documents { get; set; }
    }
}
