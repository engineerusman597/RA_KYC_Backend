using RA_KYC_BE.Application.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Application.Interfaces.TypedRepositories
{
    public interface IAccountRepository
    {
        Task<List<AppUserDto>> GetAllUsersByRole(string role);
        Task<bool> ApprovedUsers(UserApprovalDto model);
    }
}
