using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class StockTransferBase : AuditEntity
    {
        public Guid FromBranchId { get; set; }
        public Guid ToBranchId { get; set; }
        public string TransferNo { get; set; } = default!;
        public DateTime TransferDate { get; set; }
        public StockTransferStatus Status { get; set; }
    }

    public class StockTransfer : StockTransferBase
    {
        public ICollection<StockTransferItem> Items { get; set; } = new List<StockTransferItem>();
    }
}
