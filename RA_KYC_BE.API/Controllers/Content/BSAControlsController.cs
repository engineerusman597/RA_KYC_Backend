using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSAControlsController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BSAControlsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost()]
        public async Task<IActionResult> POST([FromBody] AddBSAControlDto addBSAControlDto)
        {
            var bsaControl = _mapper.Map<BSAControls>(addBSAControlDto);
            bsaControl.CreatedBy = UserId;
            bsaControl.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.BSAControls.Add(bsaControl);
            return Ok(await _unitOfWork.Complete());
        }

        /// <summary>
        /// Get BSA Control by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var bsaControls = await _unitOfWork.BSAControls.GetById(Id);
            return Ok(_mapper.Map<BSAControlsDto>(bsaControls));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var mitigatingControls = await _unitOfWork.BSAControls.GetAll();
            var bsaControlsDto = _mapper.Map<List<BSAControlsDto>>(mitigatingControls);
            return Ok(bsaControlsDto);
        }

        [HttpGet("GetAllCategoryCodes")]
        public async Task<IActionResult> GetAllCategoryCodes()
        {
            return Ok(await _unitOfWork.BSAs.GetAllCategoryCodes());
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] UpdateBSAControlsDto bsaControlsDto)
        {
            var bsaControls = await _unitOfWork.BSAControls.GetById(bsaControlsDto.Id);
            bsaControls.StrongQuestion = bsaControlsDto.StrongQuestion;
            bsaControls.AdequateQuestion = bsaControlsDto.AdequateQuestion;
            bsaControls.WeakQuestion = bsaControlsDto.WeakQuestion;
            bsaControls.ControlCode = bsaControlsDto.ControlCode;
            bsaControls.Category = bsaControlsDto.Category;
            bsaControls.IsActive = bsaControlsDto.IsActive;
            bsaControls.UpdatedBy = UserId;
            bsaControls.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var bsaControls = await _unitOfWork.BSAControls.GetById(Id);
            await _unitOfWork.BSAControls.Remove(bsaControls);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
