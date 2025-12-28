using AutoMapper;
using PMS.Core.Domain.Interfaces;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.Rooms.Domain.Enums;
using PMS.Features.RoomServiceRequests.Application.DTOS;
using PMS.Features.RoomServiceRequests.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Enums;
using PMS.Features.RoomServiceRequests.Domain.IRepositories;

namespace PMS.Features.RoomServiceRequests.Application.Services
{
    public class HousekeepingService : IHousekeepingService
    {
        private readonly IHousekeepingRepository _housekeepingRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IMapper _mapper;

        public HousekeepingService(IHousekeepingRepository housekeepingRepo, IRepository<Room> roomRepo, IMapper mapper)
        {
            _housekeepingRepo = housekeepingRepo;
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        public async Task<HousekeepingDashboardDto> GetDashboardAsync()
        {
            var rooms = await _housekeepingRepo.GetAllRoomsWithStatusAsync();

            var roomDtos = rooms.Select(r => new RoomHousekeepingDto
            {
                RoomNumber = r.RoomNumber,
                Floor = r.Floor,
                PricePerNight = r.PricePerNight,
                Type = r.Type.ToString(),
                Status = r.HousekeepingStatus.ToString(),
                IsOccupied = r.Status == RoomStatus.Occupied,
                PendingServiceRequestsCount = r.ServiceRequests.Count(sr => !sr.IsCompleted)
            }).ToList();

            return new HousekeepingDashboardDto
            {
                Rooms = roomDtos,
                StatusCounts = new Dictionary<string, int>
                {
                    { "Cleaned", rooms.Count(r => r.HousekeepingStatus == HousekeepingStatus.Cleaned) },
                    { "NeedsCleaning", rooms.Count(r => r.HousekeepingStatus == HousekeepingStatus.NeedsCleaning) },
                    { "InProgress", rooms.Count(r => r.HousekeepingStatus == HousekeepingStatus.InProgress) },
                    { "Maintenance", rooms.Count(r => r.HousekeepingStatus == HousekeepingStatus.Maintenance) }
                }
            };
        }

        public async Task<bool> CreateRequestAsync(CreateServiceRequestDto dto)
        {
            var room = await _roomRepo.GetByIdAsync(dto.RoomId);
            if (room == null) return false;

            // استخدام الـ Enum مباشرة بدل الـ String Contains لضمان الدقة
            var request = new RoomServiceRequest
            {
                RoomId = dto.RoomId,
                ServiceType = Enum.Parse<RoomServiceType>(dto.ServiceType),
                Priority = dto.Priority,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            if (request.ServiceType == RoomServiceType.Maintenance)
            {
                await _housekeepingRepo.UpdateRoomStatusAsync(dto.RoomId, HousekeepingStatus.Maintenance);
            }

            await _housekeepingRepo.AddServiceRequestAsync(request);
            return true;
        }

        public async Task<bool> UpdateRoomStatusAsync(int roomId, HousekeepingStatus newStatus)
        {
            await _housekeepingRepo.UpdateRoomStatusAsync(roomId, newStatus);
            return true;
        }
    }
}