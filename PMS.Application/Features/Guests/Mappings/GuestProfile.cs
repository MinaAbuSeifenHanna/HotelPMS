using System;
using AutoMapper;
using PMS.Application.Features.Guests.DTOs;
using PMS.Domain.Entites;

namespace PMS.Application.Features.Guests.Mappings;

public class GuestProfile : Profile
{
    public GuestProfile()
    {
        CreateMap<AddGuestDto, Guest>()
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateGuestDto, Guest>();
        CreateMap<Guest, GuestResultDto>();
    }
}
