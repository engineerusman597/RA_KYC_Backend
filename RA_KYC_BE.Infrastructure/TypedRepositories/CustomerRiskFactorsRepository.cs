using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Infrastructure.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class CustomerRiskFactorsRepository : GenericRepository<CustomerRiskFactors>, ICustomerRiskFactorsRepository
    {
        private readonly AppDbContext _context;

        public CustomerRiskFactorsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
