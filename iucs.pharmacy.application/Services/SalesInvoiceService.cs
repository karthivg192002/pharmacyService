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
        Task<ServiceResult<List<SalesInvoiceDto>>> GetAllAsync(Guid branchId);
        Task<ServiceResult<SalesInvoiceDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<List<SalesInvoiceDto>>> GetByCustomerAsync(Guid customerId);
        Task<ServiceResult<bool>> UpdateStatusAsync(Guid invoiceId, SalesInvoiceStatus status, Guid userId);
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

        public async Task<ServiceResult<List<SalesInvoiceDto>>> GetAllAsync(Guid branchId)
        {
            try
            {
                var data = await _db.SalesInvoice
                    .AsNoTracking()
                    .Where(x => x.BranchId == branchId)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Select(x => new SalesInvoiceDto
                    {
                        Id = x.Id,
                        BranchId = x.BranchId,
                        CustomerId = x.CustomerId ?? Guid.Empty,
                        PrescriptionId = x.PrescriptionId,
                        InvoiceDate = x.InvoiceDate,
                        Status = x.Status
                    })
                    .ToListAsync();

                return ServiceResult<List<SalesInvoiceDto>>.SuccessResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll sales invoices failed");
                return ServiceResult<List<SalesInvoiceDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<SalesInvoiceDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var invoice = await _db.SalesInvoice
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new SalesInvoiceDto
                    {
                        Id = x.Id,
                        BranchId = x.BranchId,
                        CustomerId = x.CustomerId ?? Guid.Empty,
                        PrescriptionId = x.PrescriptionId,
                        InvoiceDate = x.InvoiceDate,
                        Status = x.Status,

                        Items = _db.SalesInvoiceItem
                            .Where(i => i.SalesInvoiceId == x.Id)
                            .Select(i => new SalesInvoiceItemDto
                            {
                                Id = i.Id,
                                MedicineId = i.MedicineId,
                                BatchId = i.BatchId,
                                Quantity = i.Quantity,
                                SellingPrice = i.SellingPrice,
                                Mrp = i.Mrp,
                                GstPercent = i.GstPercent,
                                DiscountAmount = i.DiscountAmount ?? 0,
                                LineTotal = i.LineTotal
                            })
                            .ToList(),

                        Payments = _db.SalesPayment
                            .Where(p => p.SalesInvoiceId == x.Id)
                            .Select(p => new SalesPaymentDto
                            {
                                Id = p.Id,
                                PaymentMode = p.PaymentMode,
                                Amount = p.Amount,
                                ReferenceNo = p.ReferenceNo
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (invoice == null)
                    return ServiceResult<SalesInvoiceDto>.Failure("Sales invoice not found");

                return ServiceResult<SalesInvoiceDto>.SuccessResult(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get sales invoice failed");
                return ServiceResult<SalesInvoiceDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<List<SalesInvoiceDto>>> GetByCustomerAsync(Guid customerId)
        {
            try
            {
                var data = await _db.SalesInvoice.AsNoTracking().Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Select(x => new SalesInvoiceDto
                    {
                        Id = x.Id,
                        BranchId = x.BranchId,
                        CustomerId = x.CustomerId ?? Guid.Empty,
                        PrescriptionId = x.PrescriptionId,
                        InvoiceDate = x.InvoiceDate,
                        Status = x.Status
                    })
                    .ToListAsync();

                return ServiceResult<List<SalesInvoiceDto>>.SuccessResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByCustomer failed");
                return ServiceResult<List<SalesInvoiceDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> UpdateStatusAsync(Guid invoiceId, SalesInvoiceStatus status, Guid userId)
        {
            try
            {
                var invoice = await _db.SalesInvoice.FirstOrDefaultAsync(x => x.Id == invoiceId);

                if (invoice == null)
                    return ServiceResult<bool>.Failure("Sales invoice not found");

                invoice.Status = status;
                invoice.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Update sales invoice status db error");
                return ServiceResult<bool>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update sales invoice status error");
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
