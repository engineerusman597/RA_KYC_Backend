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
        IBSARepository BSAs { get; }
        IBSAControlRepository BSAControls { get; }
         IBSARiskMatrixRepository BSARiskMatrixs { get;}
        IOFACRepository OFACs { get; }
        IOFACControlRepository OFACControls { get; }
        IOFACRiskMatrixRepository OFACRiskMatrixs { get; }
        Task<int> Complete();
    }
}
