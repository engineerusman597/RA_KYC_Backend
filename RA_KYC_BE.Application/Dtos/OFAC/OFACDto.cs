using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.OFAC
{
    public class OFACDto
    {
        public OFACDto()
        {
            ChildrenCategories = new List<OFACControlsDto>();
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
        public string RiskCategoryNumber { get; set; }
        public string RowInFFIECAppendix { get; set; }
        public bool IsActive { get; set; }
        public List<OFACControlsDto> ChildrenCategories { get; set; }
    }
}
