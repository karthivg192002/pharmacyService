using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.pharmacy.application.Dto.Transaction
{
    public class PurchaseInvoiceDto
    {
        public Guid BranchId { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }

        public List<PurchaseInvoiceItemDto> Items { get; set; } = new();
    }

    public class PurchaseInvoiceItemDto
    {
        public Guid MedicineId { get; set; }
        public string BatchNo { get; set; } = null!;
        public DateTime? ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal Mrp { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal GstPercent { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal FreeQty { get; set; }
    }
}
