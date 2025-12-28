using AutoMapper;
using PMS.Features.Guests.Application.DTOS;
using PMS.Features.Guests.Domain.Entities;

namespace PMS.Features.Guests.Application
{
    public class GuestMappingProfile : Profile
    {
        public GuestMappingProfile()
        {
            CreateMap<AddGuestDto, Guest>();
            CreateMap<UpdateGuestDto, Guest>();

            CreateMap<Guest, GuestResultDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
