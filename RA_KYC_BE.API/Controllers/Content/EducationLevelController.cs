using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.CustomerTypes;
using RA_KYC_BE.Application.Dtos.EducationLevel;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationLevelController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EducationLevelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EducationLevelDto educationLevelDto)
        {
            var educationLevel = _mapper.Map<EducationLevel>(educationLevelDto);
            educationLevel.CreatedBy = UserId;
            educationLevel.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.EducationLevels.Add(educationLevel);
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
            var educationLevel = await _unitOfWork.EducationLevels.GetById(Id);
            return Ok(_mapper.Map<EducationLevelDto>(educationLevel));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var educationLevels = await _unitOfWork.EducationLevels.GetAll();
            return Ok(_mapper.Map<List<EducationLevelDto>>(educationLevels));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] EducationLevelDto educationLevelDto)
        {
            var educationLevel = await _unitOfWork.EducationLevels.GetById(educationLevelDto.Id);
            educationLevel.Level = educationLevelDto.Level;
            educationLevel.IsActive = educationLevelDto.IsActive;
            educationLevel.UpdatedBy = UserId;
            educationLevel.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var businessTypes = await _unitOfWork.EducationLevels.GetById(Id);
            await _unitOfWork.EducationLevels.Remove(businessTypes);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
