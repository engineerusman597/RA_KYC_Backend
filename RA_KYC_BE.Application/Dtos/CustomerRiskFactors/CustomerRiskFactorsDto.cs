using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Application.Dtos.CustomerRiskFactors
{
    public class CustomerRiskFactorsDto
    {
        public int Id { get; set; }
        public bool IsPEP { get; set; }
        [StringLength(100)]
        public string BusinessInductry { get; set; }
        public int CustomerTypeId { get; set; }
        public int BusinessTypeId { get; set; }
        [StringLength(10)]
        public string NACISCode { get; set; }
        [StringLength(10)]
        public string SourceOfFunds { get; set; }
        public decimal ExpectedMonthlyVolume { get; set; }
        public int ExpectedTransactionFrequency { get; set; }
        [StringLength(50)]
        public string PurposeOfRelationship { get; set; }
        [StringLength(2)]
        public string TaxResidenceCountryCode { get; set; }
        public bool DeniedFinancialServicesBefore { get; set; }
        public bool ConnectionToSanctionedCountries { get; set; }
        public bool PredominantlyRemoteTransactions { get; set; }
        public bool UsesHighRiskProducts { get; set; }
        [StringLength(255)]
        public string ExpectedRelationshipDuration { get; set; }
        [StringLength(255)]
        public string ReferralSource { get; set; }
        public bool HadBankruptcy { get; set; }
        public string LegalHistory { get; set; }
        public string KnownAssociates { get; set; }
        public string TravelHistory { get; set; }
        public string OrganizationMemberships { get; set; }
        public string PreviousEmployment { get; set; }
        public bool NegativePublicity { get; set; }
        public int CustomerDetailsId { get; set; }
    }
}
