using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Dtos.CustomerRiskFactors;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRiskFactorsController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerRiskFactorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerRiskFactorsDto customerRiskFactorsDto)
        {
            var customerRiskFactors = _mapper.Map<CustomerRiskFactors>(customerRiskFactorsDto);
            customerRiskFactors.CreatedBy = UserId;
            customerRiskFactors.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.CustomerRiskFactors.Add(customerRiskFactors);
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
            var customerRiskFactorsFromDB = await _unitOfWork.CustomerRiskFactors.GetById(Id);
            return Ok(_mapper.Map<CustomerRiskFactorsDto>(customerRiskFactorsFromDB));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var customerRiskFactorsFromDB = await _unitOfWork.CustomerRiskFactors.GetAll();
            return Ok(_mapper.Map<List<CustomerRiskFactorsDto>>(customerRiskFactorsFromDB));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CustomerRiskFactorsDto customerRiskFactorsDto)
        {
            var customerRiskFactorsFromDB = await _unitOfWork.CustomerRiskFactors.GetById(customerRiskFactorsDto.Id);
            customerRiskFactorsFromDB = _mapper.Map<CustomerRiskFactors>(customerRiskFactorsFromDB);
            customerRiskFactorsFromDB.UpdatedBy = UserId;
            customerRiskFactorsFromDB.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var notesFromDB = await _unitOfWork.CustomerRiskFactors.GetById(Id);
            await _unitOfWork.CustomerRiskFactors.Remove(notesFromDB);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
