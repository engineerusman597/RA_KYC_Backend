using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.OFAC;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class OFACControlsController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OFACControlsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost()]
        public async Task<IActionResult> POST([FromBody] AddOFACControlDTO addOFACControlDto)
        {
            var ofacControl = _mapper.Map<OFACControl>(addOFACControlDto);
            ofacControl.CreatedBy = UserId;
            ofacControl.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.OFACControls.Add(ofacControl);
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
            var ofacControls = await _unitOfWork.OFACControls.GetById(Id);
            return Ok(_mapper.Map<OFACControlsDto>(ofacControls));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var mitigatingControls = await _unitOfWork.OFACControls.GetAll();
            var ofacControlsDto = _mapper.Map<List<OFACControlsDto>>(mitigatingControls);
            return Ok(ofacControlsDto);
        }

        [HttpGet("GetAllOFACCategoryCodes")]
        public async Task<IActionResult> GetAllOFACCategoryCodes()
        {
            return Ok(await _unitOfWork.OFACs.GetAllOFACCategoryCodes());
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] UpdateOFACControlsDto ofacControlsDto)
        {
            var ofacControls = await _unitOfWork.OFACControls.GetById(ofacControlsDto.Id);
            ofacControls.StrongQuestion = ofacControlsDto.StrongQuestion;
            ofacControls.AdequateQuestion = ofacControlsDto.AdequateQuestion;
            ofacControls.WeakQuestion = ofacControlsDto.WeakQuestion;
            ofacControls.ControlCode = ofacControlsDto.ControlCode;
            ofacControls.Category = ofacControlsDto.Category;
            ofacControls.IsActive = ofacControlsDto.IsActive;
            ofacControls.UpdatedBy = UserId;
            ofacControls.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var bsaControls = await _unitOfWork.OFACControls.GetById(Id);
            await _unitOfWork.OFACControls.Remove(bsaControls);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
