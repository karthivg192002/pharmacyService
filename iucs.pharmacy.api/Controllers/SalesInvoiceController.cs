using iucs.pharmacy.application.Dto.Transaction;
using iucs.pharmacy.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _service;

        public SalesInvoiceController(ISalesInvoiceService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(SalesInvoiceCreateDto dto)
        {
            var sub = User?.FindFirst("sub")?.Value;

            //if (string.IsNullOrEmpty(sub))
            //    return Unauthorized("User not authenticated");

            var userId = Guid.Empty;

            var result = await _service.CreateAsync(dto, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetAll(Guid branchId)
        {
            return Ok(await _service.GetAllAsync(branchId));
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(Guid customerId)
        {
            return Ok(await _service.GetByCustomerAsync(customerId));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] SalesInvoiceStatus status)
        {
            var sub = User?.FindFirst("sub")?.Value;

            //if (string.IsNullOrEmpty(sub))
            //    return Unauthorized("User not authenticated");

            var userId = Guid.Empty;

            return Ok(await _service.UpdateStatusAsync(id, status, userId));
        }
    }
}
