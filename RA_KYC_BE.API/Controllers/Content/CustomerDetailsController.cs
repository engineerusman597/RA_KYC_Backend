using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Dtos.CustomerDetails;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerDetailsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDetailsDto customerDetailsDto)
        {
            var customerDetails = _mapper.Map<CustomerDetails>(customerDetailsDto);
            customerDetails.CreatedBy = UserId;
            customerDetails.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.CustomerDetails.Add(customerDetails);
            return Ok(await _unitOfWork.Complete());
        }

        /// <summary>
        /// Get CustomerDetails by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var customerDetailsFromDB = await _unitOfWork.CustomerDetails.GetById(Id);
            return Ok(_mapper.Map<CustomerDetailsDto>(customerDetailsFromDB));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var customerDetailsFromDB = await _unitOfWork.CustomerDetails.GetAll();
            return Ok(_mapper.Map<List<CustomerDetailsDto>>(customerDetailsFromDB));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CustomerDetailsDto customerDetailsDto)
        {
            var customerDetailsFromDB = await _unitOfWork.CustomerDetails.GetById(customerDetailsDto.Id);
            customerDetailsFromDB = _mapper.Map<CustomerDetails>(customerDetailsFromDB);
            customerDetailsFromDB.UpdatedBy = UserId;
            customerDetailsFromDB.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var notesFromDB = await _unitOfWork.CustomerDetails.GetById(Id);
            await _unitOfWork.CustomerDetails.Remove(notesFromDB);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
