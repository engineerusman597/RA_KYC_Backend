namespace RA_KYC_BE.Application.Dtos.MitigatingControls
{
    public class MitigatingControlsDto
    {
        public int Id { get; set; }
        public string ParentCode { get; set; }
        public string Category { get; set; }
        public string ControlCode { get; set; }
        public string Strong3 { get; set; }
        public string Adequate2 { get; set; }
        public string Weak1 { get; set; }
        public decimal Score { get; set; }
        public string Comments { get; set; }
        public string Documents { get; set; }
    }
}
