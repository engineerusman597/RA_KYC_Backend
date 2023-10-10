using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Dtos.BSARiskMatrix;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSARiskMatrixController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BSARiskMatrixController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var riskMatrix = await _unitOfWork.BSARiskMatrixs.GetAll();
            var riskCategoriesDtos = _mapper.Map<List<GetBSARiskMatrixDto>>(riskMatrix);
            return Ok(riskCategoriesDtos);
        }
    }
}
