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
using RA_KYC_BE.Application.Dtos.OFAC;
using RA_KYC_BE.Application.Dtos.OFACRiskMatrix;

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
            CreateMap<BSAAssessmentBasisWithClient, BSAAssessmentBasisWithClientDto>();
            CreateMap<BSAAssessmentBasisWithClientDto, BSAAssessmentBasisWithClient>();
            CreateMap<BSAControlsWithClient, BSAControlsWithClientDto>();
            CreateMap<BSAControlsWithClientDto, BSAControlsWithClient>();
            CreateMap<GetBSARiskMatrixDto, BSARiskMatrix>();
            CreateMap<BSARiskMatrix, GetBSARiskMatrixDto>();

            CreateMap<OFACDto, OFACAssessmentBasis>();
            CreateMap<OFACAssessmentBasis, OFACDto>();
            CreateMap<AddOFACAssessmentBasisDto, OFACAssessmentBasis>();
            CreateMap<OFACAssessmentBasis, AddOFACAssessmentBasisDto>();
            CreateMap<OFACControlsDto, OFACControl>();
            CreateMap<OFACControl, OFACControlsDto>();
            CreateMap<OFACAssessmentBasisWithClient, OFACAssessmentBasisWithClientDto>();
            CreateMap<OFACControlsWithClient, OFACControlsWithClientDto>();
            CreateMap<GetOFACRiskMatrixDto, OFACRiskMatrix>();
            CreateMap<OFACRiskMatrix, GetOFACRiskMatrixDto>();
            CreateMap<OFACAssessmentBasisWithClientDto, OFACAssessmentBasisWithClient>();
            CreateMap<OFACControlsWithClientDto, OFACControlsWithClient>();
        }
    }
}
