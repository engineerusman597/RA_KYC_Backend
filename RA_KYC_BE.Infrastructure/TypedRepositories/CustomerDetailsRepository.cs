using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.GenericRepositories;
using RA_KYC_BE.Domain.Entities;
using System.Linq.Expressions;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class CustomerDetailsRepository : GenericRepository<CustomerDetails>, ICustomerDetailsRepository
    {
        private readonly AppDbContext _context;

        public CustomerDetailsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
