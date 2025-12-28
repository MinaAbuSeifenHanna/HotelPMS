using AutoMapper;
using PMS.Features.Rooms.Application.DTOS;
using PMS.Features.Rooms.Domain.Entities;

namespace PMS.Features.Rooms.Application
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {

            CreateMap<Room, RoomResultDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<AddRoomDto, Room>();
        }
    }
}
