using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.pharmacy.application.Dto;
using iucs.pharmacy.domain.Entities.Masters;
using iucs.pharmacy.domain.Entities.Transaction;

namespace iucs.pharmacy.application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Medicine, MedicineDto>().ReverseMap();

            CreateMap<PurchaseInvoice, PurchaseInvoiceDto>().ReverseMap();
            CreateMap<PurchaseInvoiceItem, PurchaseInvoiceItemDto>().ReverseMap();

            CreateMap<MedicineCategory, MedicineCategoryDto>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Medicine, MedicineDto>().ReverseMap();

            CreateMap<Prescription, PrescriptionDto>().ReverseMap();
            CreateMap<PrescriptionItem, PrescriptionItemDto>().ReverseMap();
        }
    }
}
