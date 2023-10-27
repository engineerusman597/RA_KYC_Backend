using ExcelDataReader;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using RA_KYC_BE.Application.Dtos;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.GenericRepositories;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class OFACRepository : GenericRepository<OFACAssessmentBasis>, IOFACRepository
    {
        private readonly AppDbContext _context;

        public OFACRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CategoryCodesDTO>> GetAllOFACCategoryCodes() => await _context.OFACAssessmentBasis.Select(p => new CategoryCodesDTO() { Code = p.RiskCategoryCode, Name = p.RiskCategoryCode }).ToListAsync();

        public async Task<List<OFACAssessmentBasisWithClient>> GetAllOFACRABasisByClientId(int clientId)
        {
            return await _context.OFACAssessmentBasisWithClients.ToListAsync();
        }

        public async Task<List<OFACRiskMatrix>> GetMatricesByClientId(int clientId)
        {
            return await _context.OFACRiskMatrices.Where(m => clientId > 0 ? m.ClientId == clientId : true).Include(m=>m.Client).ToListAsync();
        }

        public async Task<object> SaveRiskCategoriesWithClientAndResults(List<OFACAssessmentBasisWithClient> ofacAssessmentBasisWithClient, List<OFACControlsWithClient> ofacControlsWithClient, List<OFACRiskMatrix> ofacRiskMatrices, bool isMain)
        {
            try
            {
                var clientId = ofacAssessmentBasisWithClient.Select(x => x.ClientId).FirstOrDefault();
                var riskMatrices = await _context.OFACRiskMatrices.Where(m => m.ClientId == clientId).ToListAsync();
                if (riskMatrices != null && riskMatrices.Count > 0)
                    _context.OFACRiskMatrices.RemoveRange(riskMatrices);
                if (!isMain)
                {
                    _context.OFACControlsWithClients.RemoveRange(ofacControlsWithClient);
                    _context.OFACAssessmentBasisWithClients.RemoveRange(ofacAssessmentBasisWithClient);
                }
                await _context.SaveChangesAsync();

                ofacAssessmentBasisWithClient.ForEach((x) => x.Id = 0);
                ofacControlsWithClient.ForEach((x) => { x.Id = 0; x.ClientId = clientId; });
                await _context.OFACRiskMatrices.AddRangeAsync(ofacRiskMatrices);
                await _context.OFACAssessmentBasisWithClients.AddRangeAsync(ofacAssessmentBasisWithClient);
                await _context.OFACControlsWithClients.AddRangeAsync(ofacControlsWithClient);
                return 1;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
