using AutoMapper;
using RA_KYC_BE.Application.Dtos.BusinessTypes;
using RA_KYC_BE.Application.Dtos.Clients;
using RA_KYC_BE.Application.Dtos.CustomerDetails;
using RA_KYC_BE.Application.Dtos.CustomerRiskFactors;
using RA_KYC_BE.Application.Dtos.CustomerTypes;
using RA_KYC_BE.Application.Dtos.EducationLevel;
using RA_KYC_BE.Application.Dtos.BSA;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Application.Dtos.BSARiskMatrix;

namespace RA_KYC_BE.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Clients, ClientsDto>();
            CreateMap<ClientsDto, Clients>();
            CreateMap<CustomerTypes, CustomerTypesDto>();
            CreateMap<CustomerTypesDto, CustomerTypes>();
            CreateMap<CustomerDetailsDto, CustomerDetails>();
            CreateMap<CustomerDetails, CustomerDetailsDto>();
            CreateMap<CustomerRiskFactorsDto, CustomerRiskFactors>();
            CreateMap<CustomerRiskFactors, CustomerRiskFactorsDto>();
            CreateMap<BusinessTypesDto, BusinessTypes>();
            CreateMap<BusinessTypes, BusinessTypesDto>();
            CreateMap<EducationLevelDto, EducationLevel>();
            CreateMap<EducationLevel, EducationLevelDto>();
            CreateMap<BSADto, BSAAssessmentBasis>();
            CreateMap<BSAAssessmentBasis, BSADto>();
            CreateMap<AddBSAAssessmentBasisDto, BSAAssessmentBasis>();
            CreateMap<BSAAssessmentBasis, AddBSAAssessmentBasisDto>();
            CreateMap<BSAControlsDto, BSAControls>();
            CreateMap<BSAControls, BSAControlsDto>();
            CreateMap<BSAAssessmentBasisWithClientDto, BSAAssessmentBasisWithClient>();
            CreateMap<BSAAssessmentBasisWithClient, BSAAssessmentBasisWithClientDto>();
            CreateMap<BSAControlsWithClientDto, BSAControlsWithClient>();
            CreateMap<BSAControlsWithClient, BSAControlsWithClientDto>();
            CreateMap<GetBSARiskMatrixDto, BSARiskMatrix>();
            CreateMap<BSARiskMatrix, GetBSARiskMatrixDto>();
        }
    }
}
