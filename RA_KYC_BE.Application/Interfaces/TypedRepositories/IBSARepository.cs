using RA_KYC_BE.Application.Dtos;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.Repositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.Application.Interfaces.TypedRepositories
{
    public interface IBSARepository : IGenericRepository<BSAAssessmentBasis>
    {
        Task<List<CategoryCodesDTO>> GetAllCategoryCodes();
        Task ImportMitigatingControlsFiles(ImportFilesModel importRiskCategoriesModel);
    }
}
