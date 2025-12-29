using System;
using AutoMapper;
using PMS.Features.SPA.SpaServices.Domain.Entities;
using PMS.Features.SPA.SpaServices.Domain.IRepositry;
using PMS.Features.SPA.SpaTherapists.Application.DTOS;
using PMS.Features.SPA.SpaTherapists.Domain.Entities;
using PMS.Features.SPA.SpaTherapists.Domain.IRepositry;

namespace PMS.Features.SPA.SpaTherapists.Application.Services;

public class SpaTherapistService : ISpaTherapistService
{
        private readonly ISpaTherapistRepository _spaTherapistRepository;
        private readonly IMapper _mapper;

        public SpaTherapistService(ISpaTherapistRepository spaTherapistRepository, IMapper mapper)
        {
                _spaTherapistRepository = spaTherapistRepository;
                _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateSpaTherapistDto dto)
        {
                var therapist = _mapper.Map<SpaTherapist>(dto);

                await _spaTherapistRepository.AddAsync(therapist);
                await _spaTherapistRepository.SaveChangesAsync();

                return therapist.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
                var therapist = await _spaTherapistRepository.GetByIdAsync(id);
                if (therapist == null) return false;
                therapist.IsAvailable = false;
                _spaTherapistRepository.Update(therapist);
                await _spaTherapistRepository.SaveChangesAsync();

                return true;
        }

        public async Task<IEnumerable<SpaTherapistDto>> GetAllAsync()
        {
                var therapists = await _spaTherapistRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<SpaTherapistDto>>(therapists);
        }

        public async Task<SpaTherapistDto?> GetByIdAsync(int id)
        {
                var therapist = await _spaTherapistRepository.GetByIdAsync(id);
                return therapist == null ? null : _mapper.Map<SpaTherapistDto>(therapist);
        }

        public async Task<bool> UpdateAsync(int id, UpdateSpaTherapistDto dto)
        {
                var therapist = await _spaTherapistRepository.GetByIdAsync(id);
                if (therapist == null) return false;

                _mapper.Map(dto, therapist);

                _spaTherapistRepository.Update(therapist);
                await _spaTherapistRepository.SaveChangesAsync();


                return true;
        }
}
