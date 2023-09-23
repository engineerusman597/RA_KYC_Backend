﻿using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.GenericRepositories;
using RA_KYC_BE.Application.Interfaces.TypedRepositories;
using RA_KYC_BE.Infrastructure.TypedRepositories;

namespace RA_KYC_BE.Infrastructure.GenericRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            CustomerDetails = new CustomerDetailsRepository(_context);
            CustomerRiskFactors = new CustomerRiskFactorsRepository(_context);
            MaritalStatuses = new MaritalStatusRepository(_context);
            CustomerTypes = new CustomerTypesRepository(_context);
            BusinessTypes = new BusinessTypesRepository(_context);
            Clients = new ClientsRepository(_context);
            EducationLevels = new EducationLevelRepository(_context);
            RiskCategories = new RiskCategoriesRepository(_context);
        }
        public ICustomerDetailsRepository CustomerDetails { get; private set; }
        public ICustomerRiskFactorsRepository CustomerRiskFactors { get; private set; }
        public IMaritalStatusRepository MaritalStatuses { get; private set; }
        public ICustomerTypesRepository CustomerTypes { get; private set; }
        public IBusinessTypesRepository BusinessTypes { get; private set; }
        public IClientsRepository Clients { get; private set; }
        public IEducationLevelRepository EducationLevels { get; private set; }
        public IRiskCategoriesRepository RiskCategories { get; private set; }
        public async Task<int> Complete() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
