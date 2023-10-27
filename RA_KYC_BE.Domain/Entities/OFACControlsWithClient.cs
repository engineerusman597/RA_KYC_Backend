using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("OFACControlsWithClient")]
    public class OFACControlsWithClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        public bool IsComplete { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Clients Client { get; set; }
    }
}
