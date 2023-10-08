using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.GenericRepositories;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class BSAControlRepository : GenericRepository<BSAControls>, IBSAControlRepository
    {
        private readonly AppDbContext _context;

        public BSAControlRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BSAControlsWithClient>> GetAllBSAControlsByClientId(int clientId)
        {
            return await _context.BSAControlsWithClients.ToListAsync();
        }
    }
}
