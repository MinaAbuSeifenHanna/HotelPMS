using System;
using AutoMapper;
using PMS.Features.SPA.SpaServices.Application.DTOS;
using PMS.Features.SPA.SpaServices.Domain.Entities;

namespace PMS.Features.SPA.SpaServices.Application;

public class SpaServiceMappingProfile : Profile
{
    public SpaServiceMappingProfile()
    {
        // Entity → DTO
        CreateMap<SpaService, SpaServiceDto>().ReverseMap();

        // Create DTO → Entity
        CreateMap<CreateSpaServiceDto, SpaService>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.SpaBookings, opt => opt.Ignore());

        // Update DTO → Entity
        CreateMap<UpdateSpaServiceDto, SpaService>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SpaBookings, opt => opt.Ignore());
    }
}


