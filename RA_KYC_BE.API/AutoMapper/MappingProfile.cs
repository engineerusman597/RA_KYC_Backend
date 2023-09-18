using AutoMapper;
using RA_KYC_BE.Application.Dtos.BusinessTypes;
using RA_KYC_BE.Application.Dtos.Clients;
using RA_KYC_BE.Application.Dtos.CustomerDetails;
using RA_KYC_BE.Application.Dtos.CustomerRiskFactors;
using RA_KYC_BE.Application.Dtos.CustomerTypes;
using RA_KYC_BE.Application.Dtos.EducationLevel;
using RA_KYC_BE.Domain.Entities;

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
        }
    }
}
