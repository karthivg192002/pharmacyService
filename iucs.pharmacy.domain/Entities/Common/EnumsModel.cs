using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.pharmacy.domain.Entities.Common
{
    public class EnumsModel
    {
        #region Enums
        public enum MedicineScheduleType { OTC, H, H1, X }
        public enum PurchaseOrderStatus { Draft, Ordered, Cancelled, Closed }
        public enum StockTransactionType { Purchase, Sale, SalesReturn, PurchaseReturn, Adjustment, Damage, Expiry, TransferIn, TransferOut }
        public enum SalesInvoiceStatus { Draft, Paid, Cancelled, Returned, Completed }
        public enum PaymentMode { Cash, Card, UPI, Bank }
        public enum StockDamageReason { Damaged, Expired, Broken, Lost }
        public enum StockTransferStatus { Created, Dispatched, Received, Cancelled }
        public enum AlertType { LowStock, NearExpiry, ControlledDrug, System }
        #endregion
    }
}
