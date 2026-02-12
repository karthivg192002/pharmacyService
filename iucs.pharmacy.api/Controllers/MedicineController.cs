using iucs.pharmacy.application.Dto;
using iucs.pharmacy.application.Services.MastersCurdService;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly MedicineService _service;

        public MedicineController(MedicineService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicineDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
    }
}
