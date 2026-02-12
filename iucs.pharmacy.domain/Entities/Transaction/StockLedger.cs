using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using iucs.pharmacy.domain.Entities.Masters;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class StockLedgerBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public StockTransactionType TransactionType { get; set; }
        public string? ReferenceTable { get; set; }
        public Guid? ReferenceId { get; set; }
        public decimal? QuantityIn { get; set; }
        public decimal? QuantityOut { get; set; }
        public decimal BalanceAfter { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid? PerformedBy { get; set; }
    }

    public class StockLedger : StockLedgerBase
    {
        public Medicine? Medicine { get; set; }
        public StockBatch? Batch { get; set; }
    }
}
