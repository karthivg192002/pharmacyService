using iucs.pharmacy.application.Dto.Transaction;
using iucs.pharmacy.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var userId = Guid.Parse(User.FindFirst("sub")!.Value);

            var result = await _service.CreateAsync(dto, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
