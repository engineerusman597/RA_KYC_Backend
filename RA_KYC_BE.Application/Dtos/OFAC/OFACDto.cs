using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.OFAC
{
    public class OFACDto
    {
        public OFACDto()
        {
            MitigatingControls = new List<OFACControlsDto>();
        }
        public int Id { get; set; }
        [Required]
        public string RiskCategoryCode { get; set; }
        public string RiskCategoryName { get; set; }
        public string LowRiskQuestion { get; set; }
        public string ModerateRiskQuestion { get; set; }
        public string HighRiskQuestion { get; set; }
        public string? InherentRisk { get; set; } = "";
        public double? Score { get; set; }
        public string? CalculatedRating { get; set; }
        public bool IsChecked { get; set; } = false;
        public string CheckedRisk { get; set; } = "";
        public string ResidualRisk { get; set; } = "";
        public int ResidualRiskScore { get; set; } = 0;
        public int InherentRiskScore { get; set; } = 0;
        public string MitigatingControl { get; set; } = "";
        public decimal MitigatingControlScore { get; set; } = 0;
        public bool IsComplete { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
        public List<OFACControlsDto> MitigatingControls { get; set; }
    }
}
