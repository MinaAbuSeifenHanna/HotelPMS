
using PMS.Domain.Entites;
using PMS.Application.Abstractions;
using PMS.Application.Features.Guests.DTOs;
using PMS.Application.Common;
using AutoMapper;
using System.Linq.Expressions;


namespace PMS.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IRepository<Guest> _guestRepos;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GuestService(IRepository<Guest> guestRepos, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _guestRepos = guestRepos;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<GuestResultDto>> GetGuestByIdAsync(int guestId)
        {
            var guest = await _guestRepos.GetByIdAsync(guestId);
            if (guest == null) return Result<GuestResultDto>.Failure("Guest not found");

            var dto = _mapper.Map<GuestResultDto>(guest);
            return Result<GuestResultDto>.Success(dto);
        }
        public async Task<Result<GuestResultDto>> GetGuestByNumberIdAsync(string idNumber)
        {
            var guest = _guestRepos.Find(g => g.IdNumber == idNumber).FirstOrDefault();
            if (guest == null) return Result<GuestResultDto>.Failure("Guest not found");

            var dto = _mapper.Map<GuestResultDto>(guest);
            return Result<GuestResultDto>.Success(dto);
        }

        public async Task<Result<int>> AddGuestAsync(AddGuestDto dto)
        {

            var existingGuest = await _guestRepos.ExistsAsync(g =>
                                 g.Email == dto.Email || g.IdNumber == dto.IdNumber);
            if (existingGuest)
            {
                return Result<int>.Failure("This guest is already registered in the system.");
            }
            var guest = _mapper.Map<Guest>(dto);

            await _guestRepos.AddAsync(guest);
            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(guest.Id, "Guest created successfully.");
        }

        public async Task<Result<IReadOnlyList<GuestResultDto>>> GetAllGuestsAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
        {
            Expression<Func<Guest, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(search))
            {
                string normalizedSearch = search.Trim().ToLower();
                predicate = g => g.FirstName.ToLower().Contains(normalizedSearch) ||
                                 g.LastName.ToLower().Contains(normalizedSearch) ||
                                 g.Email.ToLower().Contains(normalizedSearch) ||
                                 g.IdNumber.Contains(search);
            }
            var guests = await _guestRepos.GetAllAsync(predicate, pageNumber, pageSize);
            if (guests == null || !guests.Any())
            {
                return Result<IReadOnlyList<GuestResultDto>>.Failure("No guests found.");
            }
            var guestDtos = _mapper.Map<IReadOnlyList<GuestResultDto>>(guests);
            return Result<IReadOnlyList<GuestResultDto>>.Success(guestDtos);
        }

        public async Task<Result<string>> UpdateGuestAsync(string idNumber, UpdateGuestDto dto)
        {

            var guest = _guestRepos.Find(g => g.IdNumber == idNumber).FirstOrDefault();

            if (guest == null)
                return Result<string>.Failure("Guest not found.");

            // Check email uniqueness (excluding current guest)
            var emailExists = await _guestRepos.ExistsAsync(g =>
                g.Email == dto.Email && g.IdNumber != idNumber);

            if (emailExists)
                return Result<string>.Failure("Another guest already uses this email.");
            _mapper.Map(dto, guest);

            _guestRepos.Update(guest);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Guest updated successfully.");
        }

        // // delete guest by id number
        public async Task<Result<string>> DeleteGuestAsync(string idNumber)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var guest = _guestRepos
                    .Find(g => g.IdNumber == idNumber)
                    .FirstOrDefault();

                if (guest == null)
                    return Result<string>.Failure("Guest not found.");

                _guestRepos.Delete(guest);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return Result<string>.Success("Guest deleted successfully.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<string>.Failure("Failed to delete guest.");
            }
        }


        // // vip guest list

        //public async Task<IEnumerable<GuestResultDto>> GetVipGuestsAsync()
        //{
        //    return await _context.Guests
        //        .Where(g => g.IsVip == true) 
        //        .Select(g => new GuestResultDto
        //        {
        //            Id = g.Id,
        //            FirstName = g.FirstName,
        //            LastName = g.LastName,
        //            Email = g.Email,
        //            Phone = g.Phone,
        //            IdNumber = g.IdNumber,
        //            Nationality = g.Nationality,
        //            IsVip = g.IsVip
        //        }).ToListAsync();
        //}


    }
}
