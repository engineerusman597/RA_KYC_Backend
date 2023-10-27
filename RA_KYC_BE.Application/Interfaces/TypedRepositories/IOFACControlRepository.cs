using RA_KYC_BE.Application.Interfaces.Repositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.Application.Interfaces.TypedRepositories
{
    public interface IOFACControlRepository : IGenericRepository<OFACControl>
    {
        Task<List<OFACControlsWithClient>> GetAllOFACControlsByClientId(int clientId);
    }
}
