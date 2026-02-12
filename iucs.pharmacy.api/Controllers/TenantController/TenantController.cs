using iucs.pharmacy.application.Services.TenantService;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers.TenantController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost("migrate-pharmacy-data/{applicationCode}")]
        public async Task<IActionResult> Migrate(string applicationCode)
        {
            try
            {
                var result = await _tenantService.MigrateAsync(applicationCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
