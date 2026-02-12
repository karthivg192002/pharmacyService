using iucs.pharmacy.application.Dto;
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
    }
}
