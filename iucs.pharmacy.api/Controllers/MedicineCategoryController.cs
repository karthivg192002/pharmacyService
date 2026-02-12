using iucs.pharmacy.application.Dto;
using iucs.pharmacy.application.Services.CommonCurdService;
using iucs.pharmacy.domain.Entities.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineCategoryController : ControllerBase
    {
        private readonly ICrudService<MedicineCategoryDto, MedicineCategory> _service;

        public MedicineCategoryController(ICrudService<MedicineCategoryDto, MedicineCategory> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicineCategoryDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicineCategoryDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
