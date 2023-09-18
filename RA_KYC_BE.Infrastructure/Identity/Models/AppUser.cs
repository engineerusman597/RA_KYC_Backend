using Application.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RA_KYC_BE.Infrastructure.Identity.Models
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}