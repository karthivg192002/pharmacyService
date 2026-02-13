using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.application.Dto.Transaction
{
    public class SalesInvoiceCreateDto
    {
        public Guid BranchId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? PrescriptionId { get; set; }

        public List<SalesInvoiceItemCreateDto> Items { get; set; } = new();
        public List<SalesPaymentCreateDto> Payments { get; set; } = new();
    }

    public class SalesInvoiceItemCreateDto
    {
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Mrp { get; set; }
        public decimal GstPercent { get; set; }
        public decimal DiscountAmount { get; set; }
    }
    
    public class SalesInvoiceItemDto
    {
        public Guid Id { get; set; }
        public Guid SalesInvoiceId { get; set; }
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Mrp { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal GstPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class SalesPaymentCreateDto
    {
        public PaymentMode PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string? ReferenceNo { get; set; }
    }
    
    public class SalesPaymentDto
    {
        public Guid Id { get; set; }
        public Guid SalesInvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string? ReferenceNo { get; set; }
        public Guid? ReceivedBy { get; set; }
    }

    public class SalesInvoiceDto
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? PrescriptionId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public SalesInvoiceStatus Status { get; set; }

        public List<SalesInvoiceItemDto> Items { get; set; } = new();
        public List<SalesPaymentDto> Payments { get; set; } = new();
    }
}
