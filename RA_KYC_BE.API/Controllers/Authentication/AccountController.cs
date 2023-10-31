using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.Application.Dtos.Account;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;

namespace RA_KYC_BE.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet("GetAllUsersByRole")]
        public async Task<IActionResult> GetAllUsersByRole(string role)
        {
            return Ok(await _accountRepository.GetAllUsersByRole(role));
        }
        [HttpPost("ApprovedUsers")]
        public async Task<IActionResult> ApprovedUsers(UserApprovalDto model)
        {
            return Ok(await _accountRepository.ApprovedUsers(model));
        }
    }
}
