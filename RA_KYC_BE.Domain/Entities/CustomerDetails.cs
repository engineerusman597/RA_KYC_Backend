using RA_KYC_BE.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RA_KYC_BE.Domain.Entities
{
    [Table("CustomerDetails")]
    public class CustomerDetails : BaseEntity
    {
        public CustomerDetails()
        {
            CustomerRiskFactors = new List<CustomerRiskFactors>();
        }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset? DateOfBirth { get; set; }
        [Required]
        public string CountryOfBirth { get; set; }
        [Required]
        public int MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        [Required]
        public int EducationLevel { get; set; }
        public string ResidentialAddress { get; set; }
        public string MailingAddress { get; set; }
        public string PreviousAddress { get; set; }
        [StringLength(2)]
        public string IsoCountryCode { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(40)]
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public string SocialMediaHandles { get; set; }
        [StringLength(255)]
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public int CreditScore { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualIncome { get; set; }
        [StringLength(255)]
        public string NationalId { get; set; }
        [StringLength(255)]
        public string PassportNumber { get; set; }
        public DateTimeOffset PassportExpiryDate { get; set; }
        [StringLength(255)]
        public string DriversLicenseNumber { get; set; }
        [StringLength(255)]
        public string DualCitizenship { get; set; }
        public int NumberOfDependents { get; set; }
        [ForeignKey("Clients")]
        public int ClientId { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual ICollection<CustomerRiskFactors> CustomerRiskFactors { get; set; }
    }
}
