using AutoMapper;
using PMS.Features.Guests.Domain.Entities;
using PMS.Features.Reservations.Application.DTOS;
using PMS.Features.Reservations.Domain.Entities;

namespace PMS.Features.Reservations.Application
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<AddReservationDto, Reservation>();
            CreateMap<AddCompanionDto, Companion>();

            CreateMap<Reservation, ReservationResultDto>()
                  .ForMember(dest => dest.GuestFullName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"))
                  .ForMember(dest => dest.GuestPhone, opt => opt.MapFrom(src => src.Guest.Phone))
                  .ForMember(dest => dest.GuestIdNumber, opt => opt.MapFrom(src => src.Guest.IdNumber))
                  .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                  .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Room.Type.ToString()))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                  .ForMember(dest => dest.Companions, opt => opt.MapFrom(src => src.Companions));
        }
    }
}
