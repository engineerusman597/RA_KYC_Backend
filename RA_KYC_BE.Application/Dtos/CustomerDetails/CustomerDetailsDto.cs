using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.CustomerDetails
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
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
        public int ClientId { get; set; }
    }
}
