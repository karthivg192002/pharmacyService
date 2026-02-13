using iucs.pharmacy.application.Dto.Transaction;
using iucs.pharmacy.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoiceController : ControllerBase
    {
        private readonly IPurchaseInvoiceService _service;

        public PurchaseInvoiceController(IPurchaseInvoiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseInvoiceDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var result = await _service.CreateAsync(dto, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid branchId)
        {
            var result = await _service.GetAllAsync(branchId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, PurchaseInvoiceDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var result = await _service.UpdateAsync(id, dto, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
