using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.application.Dto.ResponseDto;
using iucs.pharmacy.application.Dto.Transaction;
using iucs.pharmacy.domain.Data;
using iucs.pharmacy.domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.application.Services
{
    public interface ISalesInvoiceService
    {
        Task<ServiceResult<Guid>> CreateAsync(SalesInvoiceCreateDto dto, Guid userId);
    }

    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<SalesInvoiceService> _logger;

        public SalesInvoiceService(AppDbContext db, ILogger<SalesInvoiceService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ServiceResult<Guid>> CreateAsync(SalesInvoiceCreateDto dto, Guid userId)
        {
            using var tx = await _db.Database.BeginTransactionAsync();

            try
            {
                var invoice = new SalesInvoice
                {
                    Id = Guid.NewGuid(),
                    BranchId = dto.BranchId,
                    CustomerId = dto.CustomerId,
                    PrescriptionId = dto.PrescriptionId,
                    InvoiceDate = DateTime.UtcNow,
                    Status = SalesInvoiceStatus.Completed,
                    CreatedBy = userId,
                    CreatedAt = DateTime.UtcNow
                };

                _db.SalesInvoice.Add(invoice);

                foreach (var item in dto.Items)
                {
                    var batch = await _db.StockBatch
                        .FirstOrDefaultAsync(x =>
                            x.Id == item.BatchId &&
                            x.BranchId == dto.BranchId);

                    if (batch == null || batch.CurrentQuantity < item.Quantity)
                        throw new Exception("Insufficient stock");

                    batch.CurrentQuantity -= item.Quantity;

                    var lineTotal = (item.Quantity * item.SellingPrice) - item.DiscountAmount;

                    var invItem = new SalesInvoiceItem
                    {
                        Id = Guid.NewGuid(),
                        SalesInvoiceId = invoice.Id,
                        MedicineId = item.MedicineId,
                        BatchId = item.BatchId,
                        Quantity = item.Quantity,
                        SellingPrice = item.SellingPrice,
                        Mrp = item.Mrp,
                        GstPercent = item.GstPercent,
                        DiscountAmount = item.DiscountAmount,
                        LineTotal = lineTotal
                    };

                    _db.SalesInvoiceItem.Add(invItem);

                    var ledger = new StockLedger
                    {
                        Id = Guid.NewGuid(),
                        BranchId = dto.BranchId,
                        MedicineId = item.MedicineId,
                        BatchId = item.BatchId,
                        TransactionType = StockTransactionType.Sale,
                        ReferenceTable = "sales_invoice",
                        ReferenceId = invoice.Id,
                        QuantityOut = item.Quantity,
                        QuantityIn = 0,
                        BalanceAfter = batch.CurrentQuantity,
                        TransactionDate = DateTime.UtcNow,
                        PerformedBy = userId
                    };

                    _db.StockLedger.Add(ledger);

                    var medicine = await _db.Medicine.Where(x => x.Id == item.MedicineId) 
                        .Select(x => new { x.IsControlled })
                        .FirstAsync();

                    if (medicine.IsControlled)
                    {
                        _db.ControlledDrugRegister.Add(
                            new ControlledDrugRegister
                            {
                                Id = Guid.NewGuid(),
                                BranchId = dto.BranchId,
                                MedicineId = item.MedicineId,
                                BatchId = item.BatchId,
                                SalesInvoiceId = invoice.Id,
                                CustomerId = dto.CustomerId,
                                Quantity = item.Quantity,
                                TransactionDate = DateTime.UtcNow,
                                PerformedBy = userId
                            });
                    }
                }

                foreach (var p in dto.Payments)
                {
                    _db.SalesPayment.Add(new SalesPayment
                    {
                        Id = Guid.NewGuid(),
                        SalesInvoiceId = invoice.Id,
                        PaymentDate = DateTime.UtcNow,
                        PaymentMode = p.PaymentMode,
                        Amount = p.Amount,
                        ReferenceNo = p.ReferenceNo,
                        ReceivedBy = userId
                    });
                }

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                return ServiceResult<Guid>.SuccessResult(invoice.Id);
            }
            catch (DbUpdateException ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "DB error in sales");

                return ServiceResult<Guid>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Sales error");

                return ServiceResult<Guid>.Failure(ex.Message);
            }
        }
    }
}
