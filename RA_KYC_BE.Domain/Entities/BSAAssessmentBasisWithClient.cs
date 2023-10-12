using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("BSAAssessmentBasisWithClient")]
    public class BSAAssessmentBasisWithClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string RiskCategoryCode { get; set; }
        public string RiskCategoryName { get; set; }
        public string LowRiskQuestion { get; set; }
        public string ModerateRiskQuestion { get; set; }
        public string HighRiskQuestion { get; set; }
        public string RiskCategoryNumber { get; set; }
        public string RowInFFIECAppendix { get; set; }
        public bool IsChecked { get; set; }
        public string  CheckedRisk  { get; set; }
        public string  ResidualRisk  { get; set; }
        public string InherentRisk { get; set; }
        public int InherentRiskScore { get; set; }
        public string MitigatingControl { get; set; }
        public bool IsComplete { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MitigatingControlScore { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Clients Client { get; set; }
    }
}
