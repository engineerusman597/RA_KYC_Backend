using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    public class BSARiskMatrix : BaseEntity
    {
        public string Code { get; set; }
        public string RowInFFIECAppendix { get; set; }
        public string CategoryNumber { get; set; }
        public string Category { get; set; }
        public string InherentRisk { get; set; }
        public string MitigatingControls { get; set; }
        public string ResidualRisk { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Clients Client { get; set; }
    }
}
