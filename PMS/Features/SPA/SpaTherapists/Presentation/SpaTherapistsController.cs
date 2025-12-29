using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Features.SPA.SpaTherapists.Application.DTOS;
using PMS.Features.SPA.SpaTherapists.Application.Services;

namespace PMS.Features.SPA.SpaTherapists.Presentation
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpaTherapistsController : ControllerBase
    {
        private readonly ISpaTherapistService _spaTherapistService;

        public SpaTherapistsController(ISpaTherapistService spaTherapistService)
        {
            _spaTherapistService = spaTherapistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var therapists = await _spaTherapistService.GetAllAsync();
            return Ok(therapists);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var therapist = await _spaTherapistService.GetByIdAsync(id);
            if (therapist == null)
                return NotFound(new { Message = "لم يتم العثور على المعالج" });

            return Ok(therapist);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpaTherapistDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _spaTherapistService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { Message = "تم إنشاء المعالج بنجاح", Id = id }
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSpaTherapistDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _spaTherapistService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { Message = "لم يتم العثور على المعالج" });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _spaTherapistService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = "لم يتم العثور على المعالج" });

            return Ok(new { Message = "تم تعطيل المعالج بنجاح" });
        }
    }
}

