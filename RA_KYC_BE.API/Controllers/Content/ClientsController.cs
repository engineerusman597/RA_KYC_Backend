using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.BusinessTypes;
using RA_KYC_BE.Application.Dtos.Clients;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClientsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientsDto clientsDto)
        {
            var clients = _mapper.Map<Clients>(clientsDto);
            clients.CreatedBy = UserId;
            clients.CreatedOn = DateTimeOffset.UtcNow;
            await _unitOfWork.Clients.Add(clients);
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
            var clients = await _unitOfWork.Clients.GetById(Id);
            return Ok(_mapper.Map<ClientsDto>(clients));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _unitOfWork.Clients.GetAll();
            return Ok(_mapper.Map<List<ClientsDto>>(clients));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] ClientsDto clientsDto)
        {
            var clients = await _unitOfWork.Clients.GetById(clientsDto.Id);
            clients = _mapper.Map<Clients>(clientsDto);
            clients.UpdatedBy = UserId;
            clients.UpdatedOn = DateTimeOffset.UtcNow;
            return Ok(await _unitOfWork.Complete());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var clients = await _unitOfWork.Clients.GetById(Id);
            await _unitOfWork.Clients.Remove(clients);
            return Ok(await _unitOfWork.Complete());
        }
    }
}
