﻿using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Infrastructure.GenericRepositories;
using RA_KYC_BE.Domain.Entities;

namespace RA_KYC_BE.Infrastructure.TypedRepositories
{
    public class CustomerTypesRepository : GenericRepository<CustomerTypes>, ICustomerTypesRepository
    {
        private readonly AppDbContext _context;

        public CustomerTypesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
