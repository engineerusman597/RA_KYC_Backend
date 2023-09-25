using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Infrastructure.GenericRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Application.Dtos.RiskCategories;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class RiskCategoriesRepository : GenericRepository<RiskCategories>, IRiskCategoriesRepository
    {
        private readonly AppDbContext _context;

        public RiskCategoriesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ChildrenRiskCategoriesDto>> GetChildrenRiskCategories()
        {
            var childrenRiskCategories = new List<ChildrenRiskCategoriesDto>()
            {
                new ChildrenRiskCategoriesDto()
                {
                    Id = 1,
                    ParentCode = "BD",
                    Code = "BD-1",
                    Name = "Policies & Procedures",
                    Strong = "Strong(3)",
                    StrongQuestion = "Policies and procedures have been developed to manage risks posed by deposit brokers.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "Policies and procedures for exist to manage risk posed by deposit brokers; however, they must be strengthened.",
                    Weak = "Weak(1)",
                    WeakQuestion = "No policies and procedures exist for to manage risks posed deposit brokers.",
                    Score = 3.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 2,
                    ParentCode = "CB",
                    Code = "CB-1",
                    Name = "Policy/Procedures",
                    Strong = "Strong(3)",
                    StrongQuestion = "Policy / Procedures are thorough, detailed and updated as necessary to reflect changes to the bank’s customer base.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "Policy / Procedures are adequate however, have not been updated to reflect changes to the bank’s customer base.",
                    Weak = "Weak(1)",
                    WeakQuestion = "Policy / Procedures do not exist or are significantly incomplete.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 3,
                    ParentCode = "CB",
                    Code = "CB-2",
                    Name = "CIP Documentary / Non-Documentary Verification",
                    Strong = "Strong(3)",
                    StrongQuestion = "The CIP verification process adequately verifies customers within a reasonable time after account opening. The process is well- tracked and exceptions are followed up in a timely manner.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "The CIP verification process is adequate, yet the exception process is poorly tracked and follow-up is not conducted in a timely manner. Exception process is poorly tracked and follow up is not timely.",
                    Weak = "Weak(1)",
                    WeakQuestion = "The CIP verification process does not allow for verification of customers within a reasonable time.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 4,
                    ParentCode = "CB",
                    Code = "CB-3",
                    Name = "Know Your Customer",
                    Strong = "Strong(3)",
                    StrongQuestion = "The KYC procedures are commensurate with the risk posed.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "KYC procedures exist but need strengthening given the risk posed by business products, services and customers.",
                    Weak = "Weak(1)",
                    WeakQuestion = "KYC procedures do not exist or exist but are incomplete.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 5,
                    ParentCode = "CB",
                    Code = "CB-4",
                    Name = "Enhanced Due Diligence",
                    Strong = "Strong(3)",
                    StrongQuestion = "The bank has automated processes to identify high-risk customers at account opening. The information is communicated to the front line.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "The bank has a limited, cumbersome and manual high-risk customer identification process.",
                    Weak = "Weak(1)",
                    WeakQuestion = "No process to identify high-risk customers at account opening.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 6,
                    ParentCode = "CB",
                    Code = "CB-5",
                    Name = "Fraud Detection",
                    Strong = "Strong(3)",
                    StrongQuestion = "A mostly automated fraud monitoring system to address electronic banking products.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "Mostly manual fraud monitoring is conducted to identify fraudulent activity in electronic banking products.",
                    Weak = "Weak(1)",
                    WeakQuestion = "No fraudulent activity monitoring of electronic banking products exist.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 7,
                    ParentCode = "CB",
                    Code = "CB-6",
                    Name = "Record Retention",
                    Strong = "Strong(3)",
                    StrongQuestion = "Records are maintained in accordance with applicable laws and regulations.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "Records are generally available but may be incomplete.",
                    Weak = "Weak(1)",
                    WeakQuestion = "There is no formal record retention policy.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 8,
                    ParentCode = "CB",
                    Code = "CB-7",
                    Name = "Risk Rating System",
                    Strong = "Strong(3)",
                    StrongQuestion = "The bank maintains an automated risk rating system.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "The bank maintains a manual risk rating system.",
                    Weak = "Weak(1)",
                    WeakQuestion = "The bank does not have a risk rating system.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 9,
                    ParentCode = "CB",
                    Code = "CB-8",
                    Name = "Special Measures",
                    Strong = "Strong(3)",
                    StrongQuestion = "The bank has procedures in place to comply with Section 311 of the USA PATRIOT Act which governs special measures.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "The bank has procedures in place to comply with Section 311 of the USA PATRIOT Act, however, the procedures need strengthening.",
                    Weak = "Weak(1)",
                    WeakQuestion = "The bank does not have procedures in place to comply with Section 311 of the USA PATRIOT Act which governs special measures.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 10,
                    ParentCode = "CB",
                    Code = "CB-9",
                    Name = "PEP Screening",
                    Strong = "Strong(3)",
                    StrongQuestion = "There is an automated process for PEP screening at onboarding and continuous screening.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "There is a highly manual process for PEP screening at onboarding and continuous screening is not available.",
                    Weak = "Weak(1)",
                    WeakQuestion = "There is no process for PEP screening.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 11,
                    ParentCode = "CB",
                    Code = "CB-10",
                    Name = "Suspicious Activity Detection",
                    Strong = "Strong(3)",
                    StrongQuestion = "Mostly automated suspicious activity monitoring system to monitor customer accounts.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "Mostly manual monitoring is conducted to identify suspicious activity to monitor customer accounts.",
                    Weak = "Weak(1)",
                    WeakQuestion = "No suspicious activity monitoring of customers referred by deposit brokers.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 12,
                    ParentCode = "CB",
                    Code = "CB-11",
                    Name = "Fraud Detection",
                    Strong = "Strong(3)",
                    StrongQuestion = "The bank has a comprehensive automated monitoring system to detect fraud in transaction interfaces (teller activity, checks processing, electronic banking, ACH).",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "The bank has a less comprehensive automated monitoring system to detect fraud in some transaction interfaces (teller activity, checks processing, electronic banking, ACH).",
                    Weak = "Weak(1)",
                    WeakQuestion = "The bank does not have an automated monitoring system to detect fraud.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 13,
                    ParentCode = "CB",
                    Code = "CB-12",
                    Name = "3rd Party Reliance",
                    Strong = "Strong(3)",
                    StrongQuestion = "The bank does not rely on third parties to perform CIP.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "The bank does rely on 3rd parties, however, due diligence was performed and adequate procedures are maintained.",
                    Weak = "Weak(1)",
                    WeakQuestion = "The bank relies on third parties to perform CIP and has not performed due diligence on those parties.",
                    Score = 1.00
                },
                new ChildrenRiskCategoriesDto()
                {
                    Id = 14,
                    ParentCode = "CB",
                    Code = "CB-13",
                    Name = "Customer Termination",
                    Strong = "Strong(3)",
                    StrongQuestion = "Management agrees to all customer termination requests from the BSA Officer.",
                    Adequate = "Adequate(2)",
                    AdequateQuestion = "Management agrees to BSA Officer requests; however, there are instances when management has requested exceptions and received CEO approval.",
                    Weak = "Weak(1)",
                    WeakQuestion = "Management has disagreed with the BSA Officer request for customer termination and failed to provide a reasonable explanation, which the CEO approved.",
                    Score = 1.00
                }
            };

            return childrenRiskCategories;
        }
    }
}
