using RA_KYC_BE.Application.Dtos;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.Repositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.Application.Interfaces.TypedRepositories
{
    public interface IOFACRepository : IGenericRepository<OFACAssessmentBasis>
    {
        Task<List<CategoryCodesDTO>> GetAllOFACCategoryCodes();
        Task<List<OFACRiskMatrix>> GetMatricesByClientId(int clientId);
        Task<List<OFACAssessmentBasisWithClient>> GetAllOFACRABasisByClientId(int clientId);
        Task<object> SaveRiskCategoriesWithClientAndResults(List<OFACAssessmentBasisWithClient> ofacAssessmentBasisWithClient, List<OFACControlsWithClient> ofacControlsWithClient, List<OFACRiskMatrix> ofacRiskMatrices, bool isMain);
    }
}
