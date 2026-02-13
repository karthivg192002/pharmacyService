using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.application.Dto.ResponseDto;
using iucs.pharmacy.domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;
using iucs.pharmacy.domain.Data;
using AutoMapper;
using iucs.pharmacy.application.Dto.Transaction;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace iucs.pharmacy.application.Services
{
    public interface IPurchaseInvoiceService
    {
        Task<ServiceResult<Guid>> CreateAsync(PurchaseInvoiceDto dto, Guid userId);
        Task<ServiceResult<List<PurchaseInvoiceDto>>> GetAllAsync(Guid branchId);
        Task<ServiceResult<PurchaseInvoiceDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<bool>> UpdateAsync(Guid id, PurchaseInvoiceDto dto, Guid userId);
    }
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<PurchaseInvoiceService> _logger;
        private readonly IMapper _mapper;

        public PurchaseInvoiceService(AppDbContext db, ILogger<PurchaseInvoiceService> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResult<Guid>> CreateAsync(PurchaseInvoiceDto dto, Guid userId)
        {
            using var trx = await _db.Database.BeginTransactionAsync();

            try
            {
                var invoice = new PurchaseInvoice
                {
                    Id = Guid.NewGuid(),
                    BranchId = dto.BranchId,
                    SupplierId = dto.SupplierId,
                    InvoiceDate = dto.InvoiceDate,
                    InvoiceNo = dto.InvoiceNo,
                    CreatedBy = userId
                };

                _db.PurchaseInvoice.Add(invoice);

                foreach (var item in dto.Items)
                {
                    var purchaseItem = new PurchaseInvoiceItem
                    {
                        Id = Guid.NewGuid(),
                        PurchaseInvoiceId = invoice.Id,
                        MedicineId = item.MedicineId,
                        BatchNo = item.BatchNo,
                        ManufactureDate = item.ManufactureDate,
                        ExpiryDate = item.ExpiryDate,
                        PurchaseRate = item.PurchaseRate,
                        Mrp = item.Mrp,
                        SellingPrice = item.SellingPrice,
                        GstPercent = item.GstPercent,
                        ReceivedQty = item.ReceivedQty,
                        FreeQty = item.FreeQty
                    };

                    _db.PurchaseInvoiceItem.Add(purchaseItem);

                    // -----------------------------
                    // Create stock batch
                    // -----------------------------

                    var batch = new StockBatch
                    {
                        Id = Guid.NewGuid(),
                        BranchId = dto.BranchId,
                        MedicineId = item.MedicineId,
                        BatchNo = item.BatchNo,
                        ManufactureDate = item.ManufactureDate,
                        ExpiryDate = item.ExpiryDate,
                        PurchaseRate = item.PurchaseRate,
                        Mrp = item.Mrp,
                        SellingPrice = item.SellingPrice,
                        GstPercent = item.GstPercent,
                        OpeningQuantity = item.ReceivedQty + (item.FreeQty),
                        CurrentQuantity = item.ReceivedQty + (item.FreeQty),
                        IsBlocked = false,
                        CreatedFromPurchaseItemId = purchaseItem.Id,
                        CreatedBy = userId
                    };

                    _db.StockBatch.Add(batch);

                    // -----------------------------
                    // Stock ledger
                    // -----------------------------

                    _db.StockLedger.Add(new StockLedger
                    {
                        Id = Guid.NewGuid(),
                        BranchId = dto.BranchId,
                        MedicineId = item.MedicineId,
                        BatchId = batch.Id,
                        TransactionType = StockTransactionType.Purchase,
                        ReferenceTable = "PurchaseInvoice",
                        ReferenceId = invoice.Id,
                        QuantityIn = batch.OpeningQuantity,
                        QuantityOut = null,
                        BalanceAfter = batch.CurrentQuantity,
                        TransactionDate = DateTime.UtcNow,
                        PerformedBy = userId,
                        CreatedBy = userId
                    });

                    // -----------------------------
                    // Controlled drug register
                    // -----------------------------

                    var medicine = await _db.Medicine
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == item.MedicineId);

                    if (medicine != null && medicine.IsControlled)
                    {
                        _db.ControlledDrugRegister.Add(new ControlledDrugRegister
                        {
                            Id = Guid.NewGuid(),
                            BranchId = dto.BranchId,
                            MedicineId = item.MedicineId,
                            BatchId = batch.Id,
                            SalesInvoiceId = Guid.Empty, // purchase side
                            CustomerId = Guid.Empty,
                            DoctorName = "N/A",
                            DoctorRegistrationNo = "N/A",
                            Quantity = batch.OpeningQuantity,
                            TransactionDate = DateTime.UtcNow,
                            PerformedBy = userId,
                            CreatedBy = userId
                        });
                    }
                }

                await _db.SaveChangesAsync();
                await trx.CommitAsync();

                return ServiceResult<Guid>.SuccessResult(invoice.Id, "Created Success");
            }
            catch (DbUpdateException ex)
            {
                await trx.RollbackAsync();
                _logger.LogError(ex, "Purchase invoice DB error");

                return ServiceResult<Guid>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                _logger.LogError(ex, "Purchase invoice failed");

                return ServiceResult<Guid>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<List<PurchaseInvoiceDto>>> GetAllAsync(Guid branchId)
        {
            try
            {
                var data = await _db.PurchaseInvoice.AsNoTracking().Where(x => x.BranchId == branchId)
                    .Include(x => x.Supplier)
                    .Include(x => x.Items!)
                        .ThenInclude(i => i.Medicine)
                    .OrderByDescending(x => x.InvoiceDate)
                    .ToListAsync();

                return ServiceResult<List<PurchaseInvoiceDto>>.SuccessResult(_mapper.Map<List<PurchaseInvoiceDto>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll purchase invoices failed");
                return ServiceResult<List<PurchaseInvoiceDto>>.Failure(ex.Message);
            }
        }

        // -------------------------------------------------
        // GET BY ID
        // -------------------------------------------------
        public async Task<ServiceResult<PurchaseInvoiceDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _db.PurchaseInvoice
                    .AsNoTracking()
                    .Include(x => x.Supplier)
                    .Include(x => x.Items!)
                        .ThenInclude(i => i.Medicine)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                    return ServiceResult<PurchaseInvoiceDto>.Failure("Invoice not found");

                return ServiceResult<PurchaseInvoiceDto>
                    .SuccessResult(_mapper.Map<PurchaseInvoiceDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get purchase invoice by id failed");
                return ServiceResult<PurchaseInvoiceDto>.Failure(ex.Message);
            }
        }

        // -------------------------------------------------
        // UPDATE
        // (simple & safe – header + reinsert items)
        // -------------------------------------------------
        public async Task<ServiceResult<bool>> UpdateAsync(
            Guid id,
            PurchaseInvoiceDto dto,
            Guid userId)
        {
            using var trx = await _db.Database.BeginTransactionAsync();

            try
            {
                var invoice = await _db.PurchaseInvoice
                    .Include(x => x.Items)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (invoice == null)
                    return ServiceResult<bool>.Failure("Invoice not found");

                // header
                invoice.InvoiceDate = dto.InvoiceDate;
                invoice.InvoiceNo = dto.InvoiceNo;
                invoice.SupplierId = dto.SupplierId;
                invoice.UpdatedAt = DateTime.UtcNow;

                // IMPORTANT NOTE:
                // Updating purchase invoices in real pharmacy systems
                // is usually restricted after stock posting.
                // For now we re-create items and stock entries.

                var oldItems = await _db.PurchaseInvoiceItem
                    .Where(x => x.PurchaseInvoiceId == id)
                    .ToListAsync();

                _db.PurchaseInvoiceItem.RemoveRange(oldItems);

                var oldBatches = await _db.StockBatch
                    .Where(x => x.CreatedFromPurchaseItemId != Guid.Empty
                             && oldItems.Select(i => i.Id)
                                        .Contains(x.CreatedFromPurchaseItemId))
                    .ToListAsync();

                _db.StockBatch.RemoveRange(oldBatches);

                var oldLedger = await _db.StockLedger
                    .Where(x => x.ReferenceId == id
                             && x.ReferenceTable == "PurchaseInvoice")
                    .ToListAsync();

                _db.StockLedger.RemoveRange(oldLedger);

                var oldControlled = await _db.ControlledDrugRegister
                    .Where(x => x.SalesInvoiceId == Guid.Empty
                             && x.TransactionDate >= invoice.CreatedAt)
                    .ToListAsync();

                _db.ControlledDrugRegister.RemoveRange(oldControlled);

                foreach (var item in dto.Items)
                {
                    var newItem = new PurchaseInvoiceItem
                    {
                        Id = Guid.NewGuid(),
                        PurchaseInvoiceId = invoice.Id,
                        MedicineId = item.MedicineId,
                        BatchNo = item.BatchNo,
                        ManufactureDate = item.ManufactureDate,
                        ExpiryDate = item.ExpiryDate,
                        PurchaseRate = item.PurchaseRate,
                        Mrp = item.Mrp,
                        SellingPrice = item.SellingPrice,
                        GstPercent = item.GstPercent,
                        ReceivedQty = item.ReceivedQty,
                        FreeQty = item.FreeQty,
                    };

                    _db.PurchaseInvoiceItem.Add(newItem);

                    var qty = item.ReceivedQty + (item.FreeQty);

                    var batch = new StockBatch
                    {
                        Id = Guid.NewGuid(),
                        BranchId = invoice.BranchId,
                        MedicineId = item.MedicineId,
                        BatchNo = item.BatchNo,
                        ManufactureDate = item.ManufactureDate,
                        ExpiryDate = item.ExpiryDate,
                        PurchaseRate = item.PurchaseRate,
                        Mrp = item.Mrp,
                        SellingPrice = item.SellingPrice,
                        GstPercent = item.GstPercent,
                        OpeningQuantity = qty,
                        CurrentQuantity = qty,
                        IsBlocked = false,
                        CreatedFromPurchaseItemId = newItem.Id,
                        CreatedBy = userId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _db.StockBatch.Add(batch);

                    _db.StockLedger.Add(new StockLedger
                    {
                        Id = Guid.NewGuid(),
                        BranchId = invoice.BranchId,
                        MedicineId = item.MedicineId,
                        BatchId = batch.Id,
                        TransactionType = StockTransactionType.Purchase,
                        ReferenceTable = "PurchaseInvoice",
                        ReferenceId = invoice.Id,
                        QuantityIn = qty,
                        BalanceAfter = qty,
                        TransactionDate = DateTime.UtcNow,
                        PerformedBy = userId,
                        CreatedBy = userId,
                        CreatedAt = DateTime.UtcNow
                    });

                    var medicine = await _db.Medicine
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == item.MedicineId);

                    if (medicine != null && medicine.IsControlled)
                    {
                        _db.ControlledDrugRegister.Add(new ControlledDrugRegister
                        {
                            Id = Guid.NewGuid(),
                            BranchId = invoice.BranchId,
                            MedicineId = item.MedicineId,
                            BatchId = batch.Id,
                            SalesInvoiceId = Guid.Empty,
                            CustomerId = Guid.Empty,
                            DoctorName = "N/A",
                            DoctorRegistrationNo = "N/A",
                            Quantity = qty,
                            TransactionDate = DateTime.UtcNow,
                            PerformedBy = userId,
                            CreatedBy = userId,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                await _db.SaveChangesAsync();
                await trx.CommitAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (DbUpdateException ex)
            {
                await trx.RollbackAsync();
                _logger.LogError(ex, "Update purchase invoice DB error");
                return ServiceResult<bool>.Failure("Database error");
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                _logger.LogError(ex, "Update purchase invoice failed");
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
