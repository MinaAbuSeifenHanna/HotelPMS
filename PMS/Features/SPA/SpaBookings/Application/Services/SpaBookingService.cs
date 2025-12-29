using System;
using System.Linq.Expressions;
using AutoMapper;
using PMS.Features.SPA.SpaBookings.Application.DTOS;
using PMS.Features.SPA.SpaBookings.Domain.Entities;
using PMS.Features.SPA.SpaBookings.Domain.Enums;
using PMS.Features.SPA.SpaBookings.Domain.IRepositry;

namespace PMS.Features.SPA.SpaBookings.Application.Services;

public class SpaBookingService : ISpaBookingService
{
    private readonly ISpaBookingRepository _spaBookingRepository;
    private readonly IMapper _mapper;


    public SpaBookingService(ISpaBookingRepository spaBookingRepository, IMapper mapper)
    {
        _spaBookingRepository = spaBookingRepository;
        _mapper = mapper;
    }


    public async Task<bool> CancelAsync(int id)
    {
        var booking = await _spaBookingRepository.GetByIdAsync(id);
        if (booking == null) return false;

        booking.Status = SpaBookingStatus.Cancelled;

        _spaBookingRepository.Update(booking);
        await _spaBookingRepository.SaveChangesAsync();


        return true;
    }

    public async Task<int> CreateAsync(CreateSpaBookingDto dto)
    {
        var booking = _mapper.Map<SpaBooking>(dto);
        await _spaBookingRepository.AddAsync(booking);
        await _spaBookingRepository.SaveChangesAsync();
        return booking.Id;
    }

    public async Task<IEnumerable<SpaBookingDto>> GetAllAsync(string? search = null)
    {
        Expression<Func<SpaBooking, bool>>? predicate = null;

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            predicate = b =>
                b.Guest.FirstName.Contains(search) ||
                b.Guest.LastName.Contains(search) ||
                b.SpaService.Name.Contains(search) ||
                b.Therapist.FullName.Contains(search) ;
        }
        var bookings = await _spaBookingRepository.GetAllWithIncludesAsync(
            predicate,
            b => b.Guest,
            b => b.SpaService,
            b => b.Therapist,
            b => b.Room
            );
        return _mapper.Map<IEnumerable<SpaBookingDto>>(bookings);
    }

    public async Task<SpaBookingDto?> GetByIdAsync(int id)
    {
        var booking = await _spaBookingRepository
        .GetByIdWithIncludesAsync(
            d => d.Id == id,
            b => b.Guest,
            b => b.SpaService,
            b => b.Therapist,
            b => b.Room
            );
        return booking == null ? null : _mapper.Map<SpaBookingDto>(booking);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSpaBookingDto dto)
    {
        var booking = await _spaBookingRepository.GetByIdAsync(id);
        if (booking == null) return false;

        _mapper.Map(dto, booking);

        _spaBookingRepository.Update(booking);
        await _spaBookingRepository.SaveChangesAsync();


        return true;
    }
}
