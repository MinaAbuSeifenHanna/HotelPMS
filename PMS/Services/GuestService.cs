//using Microsoft.EntityFrameworkCore;
//using PMS.Domain.entity;
//using PMS.Features.Guest.Application.DTOS;

//namespace PMS.Services
//{
//    public class GuestService : IGuestService
//    {
//        private readonly PMSContext _context;
//        public GuestService(PMSContext context) => _context = context;

//        public async Task<int> AddGuestAsync(AddGuestDto dto)
//        {
//           var existingGuest = await _context.Guests
//           .AnyAsync(g => g.Email == dto.Email || g.IdNumber == dto.IdNumber);

//            if (existingGuest)
//            {
//                throw new Exception("This guest is already registered in the system.");
//            }
//            var guest = new Guest
//            {
//                FirstName = dto.FirstName,
//                LastName = dto.LastName,
//                DateOfBirth = dto.DateOfBirth,
//                Nationality = dto.Nationality,
//                Email = dto.Email,
//                Phone = dto.Phone,
//                IdType = dto.IdType,
//                IdNumber = dto.IdNumber,
//                StreetAddress = dto.StreetAddress,
//                City = dto.City,
//                Country = dto.Country,
//                PostalCode = dto.PostalCode,
//                EmergencyContactName = dto.EmergencyContactName,
//                EmergencyContactPhone = dto.EmergencyContactPhone,
//                GuestPreferences = dto.GuestPreferences,
//             //   IsVip = dto.IsVip
//            };

//            _context.Guests.Add(guest);
//            await _context.SaveChangesAsync();
//            return guest.Id;
//        }

//        public async Task<IEnumerable<GuestResultDto>> GetAllGuestsAsync()
//        {
//            return await _context.Guests
//                .Select(g => new GuestResultDto
//                {
//                    Id = g.Id,
//                    FirstName = g.FirstName,
//                    LastName = g.LastName,
//                    Email = g.Email,
//                    Phone = g.Phone,
//                    IdNumber = g.IdNumber,
//                    Nationality = g.Nationality,
//              //      IsVip = g.IsVip
//                }).ToListAsync();
//        }

//        public async Task<GuestResultDto> GetGuestByIdAsync(string idNumber)
//        {
//            var g = await _context.Guests
//                .FirstOrDefaultAsync(guest => guest.IdNumber == idNumber);
//            if (g == null) return null;

//            return new GuestResultDto
//            {
//                Id = g.Id,
//                FirstName = g.FirstName,
//                LastName = g.LastName,
//                Email = g.Email,
//                Phone = g.Phone,
//                IdNumber = g.IdNumber,
//                Nationality = g.Nationality,
//              //  IsVip = g.IsVip
//            };
//        }

//        public async Task<bool> UpdateGuestAsync(string idNumber, UpdateGuestDto dto)
//        {

//            var guest = await _context.Guests
//                .FirstOrDefaultAsync(guest => guest.IdNumber == idNumber);
//            if (guest == null) return false;

            
//            guest.FirstName = dto.FirstName;
//            guest.LastName = dto.LastName;
//            guest.Email = dto.Email;
//            guest.Phone = dto.Phone;
//            guest.StreetAddress = dto.StreetAddress;
//            guest.City = dto.City;
//        //    guest.IsVip = dto.IsVip;

//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // delete guest by id number
//        public async Task<bool> DeleteGuestByIdNumberAsync(string idNumber)
//        {
      
//            var guest = await _context.Guests
//                .Include(g => g.Reservations)
//                .FirstOrDefaultAsync(g => g.IdNumber == idNumber);

//            if (guest == null) return false;

   
//            if (guest.Reservations.Any())
//            {
//                throw new Exception("Sorry, this guest is linked to registered reservations and cannot be removed from the system.");
//            }

//            _context.Guests.Remove(guest);
//            await _context.SaveChangesAsync();
//            return true;
//        }



//        // vip guest list

//        //public async Task<IEnumerable<GuestResultDto>> GetVipGuestsAsync()
//        //{
//        //    return await _context.Guests
//        //        .Where(g => g.IsVip == true) 
//        //        .Select(g => new GuestResultDto
//        //        {
//        //            Id = g.Id,
//        //            FirstName = g.FirstName,
//        //            LastName = g.LastName,
//        //            Email = g.Email,
//        //            Phone = g.Phone,
//        //            IdNumber = g.IdNumber,
//        //            Nationality = g.Nationality,
//        //            IsVip = g.IsVip
//        //        }).ToListAsync();
//        //}


//    }
//}
