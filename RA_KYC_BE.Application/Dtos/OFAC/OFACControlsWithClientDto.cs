using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Application.Dtos.OFAC
{
    public class OFACControlsWithClientDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string ControlCode { get; set; }
        public string StrongQuestion { get; set; }
        public string AdequateQuestion { get; set; }
        public string WeakQuestion { get; set; }
        public decimal? Score { get; set; }
        public string? Comments { get; set; }
        public string? Documents { get; set; }
        public int ClientId { get; set; }
        [NotMapped]
        public bool IsComplete { get; set; }
    }
}
