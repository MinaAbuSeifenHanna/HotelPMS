using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Features.SPA.SpaServices.Application.DTOS;
using PMS.Features.SPA.SpaServices.Application.Services;

namespace PMS.Features.SPA.SpaServices.Presentation
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpaServicesController : ControllerBase
    {
        private readonly ISpaServiceService _spaServiceService;

        public SpaServicesController(ISpaServiceService spaServiceService)
        {
            _spaServiceService = spaServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _spaServiceService.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _spaServiceService.GetByIdAsync(id);

            if (service == null)
                return NotFound(new { Message = "لم يتم العثور على خدمة السبا" });

            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpaServiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _spaServiceService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { Message = "تم إنشاء خدمة السبا بنجاح", Id = id }
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSpaServiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _spaServiceService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new { Message = "لم يتم العثور على خدمة السبا" });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _spaServiceService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { Message = "لم يتم العثور على خدمة السبا" });

            return Ok(new { Message = "تم تعطيل المعالج بنجاح" });
        }
    }
}