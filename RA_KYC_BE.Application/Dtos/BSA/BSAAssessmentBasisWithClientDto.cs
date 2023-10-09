namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class BSAAssessmentBasisWithClientDto
    {
        public BSAAssessmentBasisWithClientDto()
        {
            MitigatingControls = new HashSet<BSAControlsWithClientDto>();
        }
        public int Id { get; set; }
        public string RiskCategoryCode { get; set; }
        public string RiskCategoryName { get; set; }
        public string LowRiskQuestion { get; set; }
        public string ModerateRiskQuestion { get; set; }
        public string HighRiskQuestion { get; set; }
        public string RiskCategoryNumber { get; set; }
        public string RowInFFIECAppendix { get; set; }
        public bool IsChecked { get; set; }
        public string CheckedRisk { get; set; }
        public string ResidualRisk { get; set; }
        public string InherentRisk { get; set; }
        public int InherentRiskScore { get; set; }
        public string MitigatingControl { get; set; }
        public decimal MitigatingControlScore { get; set; }
        public bool IsComplete { get; set; }
        public int ClientId { get; set; }
        public ICollection<BSAControlsWithClientDto> MitigatingControls { get; set; }
    }
}
