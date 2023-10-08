using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    public class BSAControls : BaseEntity
    {
        public string Code { get; set; }
        public string Category { get; set; }
        public string ControlCode { get; set; }
        public string StrongQuestion { get; set; }
        public string AdequateQuestion { get; set; }
        public string WeakQuestion { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Score { get; set; }
        public string? Comments { get; set; }
        public string? Documents { get; set; }
    }
}
