using AutoMapper;
using iucs.pharmacy.application.Dto;
using iucs.pharmacy.application.Services.CommonCurdService;
using iucs.pharmacy.domain.Data;
using iucs.pharmacy.domain.Entities.Masters;
using Microsoft.Extensions.Logging;

namespace iucs.pharmacy.application.Services.MastersCurdService
{
    public class MedicineCategoryService : CrudService<MedicineCategoryDto, MedicineCategory>
    {
        public MedicineCategoryService(AppDbContext db, IMapper mapper, ILogger<MedicineCategoryService> logger) : base(db, mapper, logger)
        {
        }
    }
}
