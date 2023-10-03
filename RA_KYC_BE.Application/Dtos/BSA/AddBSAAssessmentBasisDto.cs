using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class AddBSAAssessmentBasisDto
    {
        public int Id { get; set; }
        [Required]
        public string RiskCategoryCode { get; set; }
        public string RiskCategoryName { get; set; }
        public string LowRiskQuestion { get; set; }
        public string ModerateRiskQuestion { get; set; }
        public string HighRiskQuestion { get; set; }
        public bool IsActive { get; set; }
    }
}
