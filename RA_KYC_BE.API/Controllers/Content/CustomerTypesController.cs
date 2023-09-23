using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.CustomerTypes;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypesController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerTypesDto customerTypesDto)
        {
            var customerTypes = _mapper.Map<CustomerTypes>(customerTypesDto);
            customerTypes.CreatedBy = UserId;
            customerTypes.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.CustomerTypes.Add(customerTypes);
            return Ok(await _unitOfWork.Complete());
        }

        /// <summary>
        /// Get Customer Type by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var customerTypes = await _unitOfWork.CustomerTypes.GetById(Id);
            return Ok(_mapper.Map<CustomerTypesDto>(customerTypes));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var customerTypes = await _unitOfWork.CustomerTypes.GetAll();
            return Ok(_mapper.Map<List<CustomerTypesDto>>(customerTypes));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CustomerTypesDto customerTypesDto)
        {
            var customerTypesFromDB = await _unitOfWork.CustomerTypes.GetById(customerTypesDto.Id);
            customerTypesFromDB.Name = customerTypesDto.Name;
            customerTypesFromDB.IsActive = customerTypesDto.IsActive;
            customerTypesFromDB.UpdatedBy = UserId;
            customerTypesFromDB.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var customerTypes = await _unitOfWork.CustomerTypes.GetById(Id);
            await _unitOfWork.CustomerTypes.Remove(customerTypes);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
