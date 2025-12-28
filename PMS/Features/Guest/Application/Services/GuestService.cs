using AutoMapper;
using PMS.Features.Guests.Application.Services;
using PMS.Features.Guests.Application.DTOS;
using PMS.Features.Guests.Domain.IRepositories;
using PMS.Features.Reservations.Domain.Enums;
using PMS.Features.Guests.Domain.Entities;

namespace PMS.Features.Guests.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GuestService(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<int> AddGuestAsync(AddGuestDto dto)
        {
            if (!await _guestRepository.IsEmailOrIdUniqueAsync(dto.Email, dto.IdNumber))
                throw new Exception("The guest is already registered with the same email or ID number.");

            var guest = _mapper.Map<Guest>(dto);
            await _guestRepository.AddAsync(guest);
            await _guestRepository.SaveChangesAsync();
            return guest.Id;
        }

        public async Task<IEnumerable<GuestResultDto>> GetAllGuestsAsync()
        {
            var guests = await _guestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GuestResultDto>>(guests);
        }

        public async Task<GuestResultDto> GetGuestByIdAsync(string idNumber)
        {
            var guest = await _guestRepository.GetByIdNumberAsync(idNumber);
            return _mapper.Map<GuestResultDto>(guest);
        }

        public async Task<bool> UpdateGuestAsync(string idNumber, UpdateGuestDto dto)
        {
            var guest = await _guestRepository.GetByIdNumberAsync(idNumber);
            if (guest == null) return false;

            _mapper.Map(dto, guest); 
            _guestRepository.Update(guest);
            await _guestRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGuestByIdNumberAsync(string idNumber)
        {
            var guest = await _guestRepository.GetByIdNumberAsync(idNumber);
            if (guest == null) return false;

            if (guest.Reservations.Any(r => r.Status != ReservationStatus.CheckedOut))
                throw new Exception("A guest with active reservations cannot be deleted.");

            _guestRepository.Delete(guest);
            await _guestRepository.SaveChangesAsync();
            return true;
        }
    
    }
}
