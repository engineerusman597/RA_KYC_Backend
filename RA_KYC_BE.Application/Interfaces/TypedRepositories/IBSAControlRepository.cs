using RA_KYC_BE.Application.Interfaces.Repositories;
using RA_KYC_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA_KYC_BE.Application.Interfaces.TypedRepositories
{
    public interface IBSAControlRepository : IGenericRepository<BSAControls>
    {
    }
}
