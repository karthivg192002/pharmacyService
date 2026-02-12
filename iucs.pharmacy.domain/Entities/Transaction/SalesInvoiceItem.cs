using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using iucs.pharmacy.domain.Entities.Masters;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class SalesInvoiceItemBase : BaseEntity
    {
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

    public class SalesInvoiceItem : SalesInvoiceItemBase
    {
        public SalesInvoice? SalesInvoice { get; set; }
        public Medicine? Medicine { get; set; }
        public StockBatch? Batch { get; set; }
    }
}
