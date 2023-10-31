using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Application.Dtos.Account
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsApproved { get; set; }
        public string Email { get; set; }
    }
}
