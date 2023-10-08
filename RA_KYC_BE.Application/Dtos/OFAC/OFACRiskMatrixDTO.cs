namespace RA_KYC_BE.Application.Dtos.OFAC
{
    public class OFACRiskMatrixDTO
    {
        public string Code { get; set; }
        public string RowInFFIECAppendix { get; set; }
        public string CategoryNumber { get; set; }
        public string Category { get; set; }
        public string InherentRisk { get; set; }
        public string MitigatingControls { get; set; }
        public string ResidualRisk { get; set; }
    }
}
