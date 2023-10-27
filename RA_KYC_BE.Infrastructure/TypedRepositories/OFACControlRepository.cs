using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.GenericRepositories;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class OFACControlRepository : GenericRepository<OFACControl>, IOFACControlRepository
    {
        private readonly AppDbContext _context;

        public OFACControlRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OFACControlsWithClient>> GetAllOFACControlsByClientId(int clientId)
        {
            return await _context.OFACControlsWithClients.ToListAsync();
        }
    }
}
