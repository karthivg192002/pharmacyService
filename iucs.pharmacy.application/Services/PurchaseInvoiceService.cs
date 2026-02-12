using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.application.Dto.ResponseDto;
using iucs.pharmacy.application.Dto;
using iucs.pharmacy.domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;
using iucs.pharmacy.domain.Data;
using AutoMapper;

namespace iucs.pharmacy.application.Services
{
    public interface IPurchaseInvoiceService
    {
        Task<ServiceResult<Guid>> CreateAsync(PurchaseInvoiceDto dto, Guid userId);
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
            using var tx = await _db.Database.BeginTransactionAsync();

            try
            {
                var invoice = _mapper.Map<PurchaseInvoice>(dto);

                //var invoice = new PurchaseInvoice
                //{
                //    Id = Guid.NewGuid(),
                //    BranchId = dto.BranchId,
                //    SupplierId = dto.SupplierId,
                //    InvoiceNo = dto.InvoiceNo,
                //    InvoiceDate = dto.InvoiceDate,
                //    ReceivedDate = DateTime.UtcNow,
                //    CreatedBy = userId,
                //    CreatedAt = DateTime.UtcNow
                //};

                _db.PurchaseInvoice.Add(invoice);

                foreach (var item in dto.Items)
                {
                    var invoiceItem = new PurchaseInvoiceItem
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
                        LineTotal = item.ReceivedQty * item.PurchaseRate
                    };

                    _db.PurchaseInvoiceItem.Add(invoiceItem);

                    // ---------- STOCK BATCH ----------
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
                        OpeningQuantity = item.ReceivedQty + item.FreeQty,
                        CurrentQuantity = item.ReceivedQty + item.FreeQty,
                        CreatedFromPurchaseItemId = invoiceItem.Id,
                        CreatedAt = DateTime.UtcNow
                    };

                    _db.StockBatch.Add(batch);

                    // ---------- STOCK LEDGER ----------
                    var ledger = new StockLedger
                    {
                        Id = Guid.NewGuid(),
                        BranchId = dto.BranchId,
                        MedicineId = item.MedicineId,
                        BatchId = batch.Id,
                        TransactionType = StockTransactionType.Purchase,
                        ReferenceTable = "purchase_invoice",
                        ReferenceId = invoice.Id,
                        QuantityIn = batch.OpeningQuantity,
                        QuantityOut = 0,
                        BalanceAfter = batch.CurrentQuantity,
                        TransactionDate = DateTime.UtcNow,
                        PerformedBy = userId
                    };

                    _db.StockLedger.Add(ledger);
                }

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                return new ServiceResult<Guid>
                {
                    Success = true,
                    Data = invoice.Id
                };
            }
            catch (DbUpdateException ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "DB error while creating purchase invoice");

                return new ServiceResult<Guid>
                {
                    Success = false,
                    Message = "Database error occurred"
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error while creating purchase invoice");

                return new ServiceResult<Guid>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
