namespace RA_KYC_BE.Application.Dtos.OFACRiskMatrix
{
    public class GetOFACRiskMatrixDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string InherentRisk { get; set; }
        public string MitigatingControls { get; set; }
        public string ResidualRisk { get; set; }
        public int ClientId { get; set; }
    }
}
