using RA_KYC_BE.Application.Dtos.BSA;

namespace RA_KYC_BE.Application.Dtos.OFAC
{
    public class UpdateOFACControlsDto
    {
        public int Id { get; set; }
        public CategoryCodes Code { get; set; }
        public string ControlCode { get; set; }
        public string Category { get; set; }
        public string StrongQuestion { get; set; }
        public string AdequateQuestion { get; set; }
        public string WeakQuestion { get; set; }
        public double? Score { get; set; }
        public bool IsActive { get; set; }
    }
}
