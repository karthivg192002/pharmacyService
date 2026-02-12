using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.pharmacy.application.Dto;
using iucs.pharmacy.application.Services.CommonCurdService;
using iucs.pharmacy.domain.Data;
using iucs.pharmacy.domain.Entities.Masters;
using Microsoft.Extensions.Logging;

namespace iucs.pharmacy.application.Services.MastersCurdService
{
    public class PrescriptionService : CrudService<PrescriptionDto, Prescription>
    {
        public PrescriptionService(AppDbContext db, IMapper mapper, ILogger<PrescriptionService> logger) : base(db, mapper, logger)
        {
        }
    }
}
