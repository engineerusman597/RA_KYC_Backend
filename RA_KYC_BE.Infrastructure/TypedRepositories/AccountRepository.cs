using Application.Interfaces;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RA_KYC_BE.Application.Dtos.Account;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailService _emailSender;
        private readonly IUnitOfWork _unitOfWork;

        public AccountRepository(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IEmailService emailSender, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<AppUserDto>> GetAllUsersByRole(string role)
        {
            try
            {
                List<AppUserDto> appUsers = new List<AppUserDto>();
                var users = await _userManager.GetUsersInRoleAsync(role);
                appUsers.AddRange(users.Select(x => new AppUserDto
                {
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                    IsApproved = x.IsApproved
                }));
                return appUsers;
            }
            catch (Exception)
            {
                return new List<AppUserDto>();
            }
        }
        public async Task<bool> ApprovedUsers(UserApprovalDto model)
        {
            try
            {
               var users= _userManager.Users.Where(x=>model.Ids.Contains(x.Id)).ToList();
                users.ForEach(x =>
                {
                    x.IsApproved = model.IsApproved;
                });

                await _unitOfWork.Complete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
