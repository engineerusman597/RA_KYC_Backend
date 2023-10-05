using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.BSA
{
    public class AddBSAControlDto
    {
        public CategoryCodes Code { get; set; }
        public string Category { get; set; }
        public string ControlCode { get; set; }
        public string StrongQuestion { get; set; }
        public string AdequateQuestion { get; set; }
        public string WeakQuestion { get; set; }
        public bool IsActive { get; set; }
    }
}
