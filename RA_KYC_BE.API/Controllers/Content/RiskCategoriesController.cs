using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.RiskCategories;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskCategoriesController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RiskCategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RiskCategoriesDto riskCategoriesDto)
        {
            var riskCategories = _mapper.Map<RiskCategories>(riskCategoriesDto);
            riskCategories.CreatedBy = UserId;
            riskCategories.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.RiskCategories.Add(riskCategories);
            return Ok(await _unitOfWork.Complete());
        }

        /// <summary>
        /// Get Customer Risk Factors by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var riskCategories = await _unitOfWork.RiskCategories.GetById(Id);
            return Ok(_mapper.Map<RiskCategoriesDto>(riskCategories));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var riskCategories = await _unitOfWork.RiskCategories.GetAll();
            return Ok(_mapper.Map<List<RiskCategoriesDto>>(riskCategories));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] RiskCategoriesDto riskCategoriesDto)
        {
            var riskCategories = await _unitOfWork.RiskCategories.GetById(riskCategoriesDto.Id);
            riskCategories.RiskCategoryName = riskCategoriesDto.RiskCategoryName;
            riskCategories.IsActive = riskCategoriesDto.IsActive;
            riskCategories.UpdatedBy = UserId;
            riskCategories.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var riskCategories = await _unitOfWork.RiskCategories.GetById(Id);
            await _unitOfWork.RiskCategories.Remove(riskCategories);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
