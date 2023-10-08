using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("Clients")]
    public class Clients : BaseEntity
    {
        public Clients()
        {
            CustomerDetails = new HashSet<CustomerDetails>();
            BSAAssessmentBasisWithClients = new HashSet<BSAAssessmentBasisWithClient>();
            BSAControlsWithClients = new HashSet<BSAControlsWithClient>();
        }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public virtual ICollection<CustomerDetails> CustomerDetails { get; set; }
        public virtual ICollection<BSAAssessmentBasisWithClient> BSAAssessmentBasisWithClients { get; set; }
        public virtual ICollection<BSAControlsWithClient> BSAControlsWithClients { get; set; }
    }
}
