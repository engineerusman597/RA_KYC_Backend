using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.GenericRepositories;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class OFACRiskMatrixRepository : GenericRepository<OFACRiskMatrix>, IOFACRiskMatrixRepository
    {
        private readonly AppDbContext _context;

        public OFACRiskMatrixRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
