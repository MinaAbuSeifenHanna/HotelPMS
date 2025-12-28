using AutoMapper;
using PMS.Features.Reservations.Domain.Enums;
using PMS.Features.Rooms.Application.DTOS;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.Rooms.Domain.Enums;
using PMS.Features.Rooms.Domain.IRepositories;

namespace PMS.Features.Rooms.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<int> AddRoomAsync(AddRoomDto dto)
        {
            if (await _roomRepository.ExistsAsync(dto.RoomNumber))
                throw new Exception("Room number already exists.");

          
            var room = _mapper.Map<Room>(dto);

            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();
            return room.Id;
        }



        public async Task<bool> DeleteRoomByNumberAsync(string roomNumber)
        {
            var room = await _roomRepository.GetRoomByNumberAsync(roomNumber);
            if (room == null) return false;

            if (room.Reservations.Any(res => res.Status != ReservationStatus.CheckedOut))
                throw new Exception("Cannot delete room with active reservations.");

            _roomRepository.Delete(room);
            await _roomRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeRoomStatusAsync(string roomNumber, RoomStatus newStatus)
        {
            var room = await _roomRepository.GetRoomByNumberAsync(roomNumber);
            if (room == null) return false;

            room.Status = newStatus;
            _roomRepository.Update(room);
            await _roomRepository.SaveChangesAsync();
            return true;
        }

        public async Task<RoomStatisticsDto> GetRoomsStatisticsAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();

            return new RoomStatisticsDto
            {
                Available = rooms.Count(r => r.Status == RoomStatus.Available),
                Occupied = rooms.Count(r => r.Status == RoomStatus.Occupied),
                Reserved = rooms.Count(r => r.Status == RoomStatus.Reserved),
                Cleaning = rooms.Count(r => r.Status == RoomStatus.Cleaning),
                Maintenance = rooms.Count(r => r.Status == RoomStatus.OutOfService)
            };
        }

        public async Task<IEnumerable<RoomResultDto>> SearchRoomsAsync(string? roomNumber, RoomType? type)
        {
            var rooms = await _roomRepository.SearchRoomsAsync(roomNumber, type);
            return _mapper.Map<IEnumerable<RoomResultDto>>(rooms);
        }
    }
}
