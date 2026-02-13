using iucs.pharmacy.application.Dto;
using iucs.pharmacy.application.Services.CommonCurdService;
using iucs.pharmacy.domain.Entities.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers.Common
{
    [Route("api/[controller]")]
    public class ManufacturerController : MasterCrudController<ManufacturerDto, Manufacturer>
    {
        public ManufacturerController(ICrudService<ManufacturerDto, Manufacturer> service) : base(service)
        {
        }
    }
}
