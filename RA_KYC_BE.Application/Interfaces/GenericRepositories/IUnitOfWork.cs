using RA_KYC_BE.Application.Interfaces.TypedRepositories;

namespace RA_KYC_BE.Application.Interfaces.GenericRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerDetailsRepository CustomerDetails { get; }
        ICustomerRiskFactorsRepository CustomerRiskFactors { get; }
        IMaritalStatusRepository MaritalStatuses { get; }
        ICustomerTypesRepository CustomerTypes { get; }
        IClientsRepository Clients { get; }
        IBusinessTypesRepository BusinessTypes { get; }
        IEducationLevelRepository EducationLevels { get; }
        Task<int> Complete();
    }
}
