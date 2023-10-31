using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Application.Dtos.Account
{
    public class UserApprovalDto
    {
        public List<int> Ids { get; set; }
        public bool IsApproved { get; set; }
    }
}
