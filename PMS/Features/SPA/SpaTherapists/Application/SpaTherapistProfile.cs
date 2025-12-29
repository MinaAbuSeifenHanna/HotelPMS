using System;
using AutoMapper;
using PMS.Features.SPA.SpaTherapists.Application.DTOS;
using PMS.Features.SPA.SpaTherapists.Domain.Entities;

namespace PMS.Features.SPA.SpaTherapists.Application;

public class SpaTherapistProfile : Profile
{
    public SpaTherapistProfile()
    {
        // Entity → DTO
        CreateMap<SpaTherapist, SpaTherapistDto>().ReverseMap();

        // Create → Entity
        CreateMap<CreateSpaTherapistDto, SpaTherapist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.SpaBookings, opt => opt.Ignore());

        // Update → Entity
        CreateMap<UpdateSpaTherapistDto, SpaTherapist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SpaBookings, opt => opt.Ignore());
    }
}
