using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.BusinessTypes;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessTypesController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BusinessTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessTypesDto businessTypesDto)
        {
            var businessTypes = _mapper.Map<BusinessTypes>(businessTypesDto);
            businessTypes.CreatedBy = UserId;
            businessTypes.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.BusinessTypes.Add(businessTypes);
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
            var businessTypesFromDB = await _unitOfWork.BusinessTypes.GetById(Id);
            return Ok(_mapper.Map<BusinessTypesDto>(businessTypesFromDB));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var businessTypesFromDB = await _unitOfWork.BusinessTypes.GetAll();
            return Ok(_mapper.Map<List<BusinessTypesDto>>(businessTypesFromDB));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] BusinessTypesDto businessTypesDto)
        {
            var businessTypesFromDB = await _unitOfWork.BusinessTypes.GetById(businessTypesDto.Id);
            businessTypesFromDB = _mapper.Map<BusinessTypes>(businessTypesDto);
            businessTypesFromDB.UpdatedBy = UserId;
            businessTypesFromDB.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var businessTypes = await _unitOfWork.BusinessTypes.GetById(Id);
            await _unitOfWork.BusinessTypes.Remove(businessTypes);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
