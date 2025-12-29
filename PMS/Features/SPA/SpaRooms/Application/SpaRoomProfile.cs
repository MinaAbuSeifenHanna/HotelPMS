using System;
using AutoMapper;
using PMS.Features.SPA.SpaRooms.Application.DTOS;
using PMS.Features.SPA.SpaServices.Application.DTOS;
using PMS.Features.SPA.SpaServices.Domain.Entities;

namespace PMS.Features.SPA.SpaRooms.Application;

public class SpaRoomMappingProfile : Profile
{
   public SpaRoomMappingProfile()
        {
            // Entity → DTO
            CreateMap<SpaRoom, SpaRoomDto>().ReverseMap();

            // Create → Entity
            CreateMap<CreateSpaRoomDto, SpaRoom>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.SpaBookings, opt => opt.Ignore());

            // Update → Entity
            CreateMap<UpdateSpaRoomDto, SpaRoom>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SpaBookings, opt => opt.Ignore());
        }
}


