using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class BSADto
    {
        public BSADto()
        {
            ChildrenCategories = new List<BSAControlsDto>();
        }
        public int Id { get; set; }
        [Required]
        public string RiskCategoryCode { get; set; }
        public string RiskCategoryName { get; set; }
        public string LowRiskQuestion { get; set; }
        public string ModerateRiskQuestion { get; set; }
        public string HighRiskQuestion { get; set; }
        public string InherentRisk { get; set; }
        public double Score { get; set; }
        public string CalculatedRating { get; set; }
        public bool IsActive { get; set; }
        public List<BSAControlsDto> ChildrenCategories { get; set; }
    }
}
