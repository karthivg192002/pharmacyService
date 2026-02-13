using iucs.pharmacy.application.Dto;
using iucs.pharmacy.application.Services.CommonCurdService;
using iucs.pharmacy.domain.Entities.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.pharmacy.api.Controllers.Common
{
    [Route("api/[controller]")]
    public class MedicineCategoryController : MasterCrudController<MedicineCategoryDto, MedicineCategory>
    {
        public MedicineCategoryController(ICrudService<MedicineCategoryDto, MedicineCategory> service) : base(service)
        {
        }
    }
}
