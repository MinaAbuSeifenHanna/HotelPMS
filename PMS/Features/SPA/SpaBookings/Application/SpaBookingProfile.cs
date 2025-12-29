using System;
using AutoMapper;
using PMS.Features.SPA.SpaBookings.Application.DTOS;
using PMS.Features.SPA.SpaBookings.Domain.Entities;
using PMS.Features.SPA.SpaBookings.Domain.Enums;

namespace PMS.Features.SPA.SpaBookings.Application;

public class SpaBookingProfile : Profile
{
    public SpaBookingProfile()
    {
        // Entity → DTO
        CreateMap<SpaBooking, SpaBookingDto>()
            .ForMember(dest => dest.GuestName,
                opt => opt.MapFrom(src => src.Guest.FirstName + " " + src.Guest.LastName))
            .ForMember(dest => dest.SpaServiceName,
                opt => opt.MapFrom(src => src.SpaService.Name))
            .ForMember(dest => dest.TherapistName,
                opt => opt.MapFrom(src => src.Therapist.FullName))
            .ForMember(dest => dest.RoomName,
                opt => opt.MapFrom(src => src.Room.RoomName));

        CreateMap<SpaBookingDto, SpaBooking>();


        // Create → Entity
        CreateMap<CreateSpaBookingDto, SpaBooking>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => SpaBookingStatus.Pending))
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.SpaService, opt => opt.Ignore())
            .ForMember(dest => dest.Therapist, opt => opt.Ignore())
            .ForMember(dest => dest.Room, opt => opt.Ignore());

        // Update → Entity
        CreateMap<UpdateSpaBookingDto, SpaBooking>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GuestId, opt => opt.Ignore())
            .ForMember(dest => dest.SpaServiceId, opt => opt.Ignore())
            .ForMember(dest => dest.Guest, opt => opt.Ignore())
            .ForMember(dest => dest.SpaService, opt => opt.Ignore())
            .ForMember(dest => dest.Therapist, opt => opt.Ignore())
            .ForMember(dest => dest.Room, opt => opt.Ignore());
    }
}
