using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Implementation
{
    public class StocktakeServiceImpl : IStocktakeService
    {
        private readonly SchoolManagementDb _context;
        private readonly IStockTransactionLogService _transactionLog;

        public StocktakeServiceImpl(SchoolManagementDb context, IStockTransactionLogService transactionLog)
        {
            _context = context;
            _transactionLog = transactionLog;
        }

        public ResponseResult<StocktakeHeaderResponseVM> CreateStocktake(StocktakeHeaderRequestVM request, int tenantId, int userId)
        {
            try
            {
                var header = new MStocktakeHeader
                {
                    StocktakeNumber = request.StocktakeNumber,
                    StocktakeDate = request.StocktakeDate,
                    BranchId = request.BranchId,
                    Notes = request.Notes,
                    CountedBy = request.CountedBy,
                    Status = "DRAFT",
                    TenantId = tenantId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _context.StocktakeHeaders.Add(header);
                _context.SaveChanges();

                return GetStocktakeById(header.Id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<StocktakeHeaderResponseVM> GetStocktakeById(int id, int tenantId)
        {
            try
            {
                var h = _context.StocktakeHeaders
                    .Include(x => x.Branch)
                    .Include(x => x.CountedByUser)
                    .Include(x => x.ApprovedByUser)
                    .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

                if (h == null)
                    return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.NotFound, null, "Stocktake not found");

                var data = new StocktakeHeaderResponseVM
                {
                    Id = h.Id,
                    StocktakeNumber = h.StocktakeNumber,
                    StocktakeDate = h.StocktakeDate,
                    BranchId = h.BranchId,
                    BranchName = h.Branch?.Name ?? "Unknown",
                    Notes = h.Notes,
                    Status = h.Status,
                    CountedBy = h.CountedBy,
                    CountedByName = h.CountedByUser != null ? $"{h.CountedByUser.FirstName} {h.CountedByUser.LastName}" : null,
                    ApprovedBy = h.ApprovedBy,
                    ApprovedByName = h.ApprovedByUser != null ? $"{h.ApprovedByUser.FirstName} {h.ApprovedByUser.LastName}" : null,
                    ApprovalDate = h.ApprovalDate,
                    CreatedOn = h.CreatedOn
                };

                return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<StocktakeHeaderResponseVM>> GetAllStocktakes(int tenantId, int? branchId = null, string? status = null)
        {
            try
            {
                var query = _context.StocktakeHeaders
                    .Include(x => x.Branch)
                    .Where(x => x.TenantId == tenantId && !x.IsDeleted);

                if (branchId.HasValue)
                    query = query.Where(x => x.BranchId == branchId);

                if (!string.IsNullOrEmpty(status))
                    query = query.Where(x => x.Status == status.ToUpper());

                var data = query.OrderByDescending(x => x.StocktakeDate)
                    .Select(h => new StocktakeHeaderResponseVM
                    {
                        Id = h.Id,
                        StocktakeNumber = h.StocktakeNumber,
                        StocktakeDate = h.StocktakeDate,
                        BranchId = h.BranchId,
                        BranchName = h.Branch.Name,
                        Notes = h.Notes,
                        Status = h.Status,
                        CreatedOn = h.CreatedOn
                    }).ToList();

                return new ResponseResult<List<StocktakeHeaderResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<StocktakeHeaderResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<StocktakeHeaderResponseVM> UpdateStocktake(int id, StocktakeHeaderRequestVM request, int tenantId, int userId)
        {
            try
            {
                var header = _context.StocktakeHeaders.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId);
                if (header == null)
                    return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.NotFound, null, "Stocktake not found");

                if (header.Status != "DRAFT" && header.Status != "IN_PROGRESS")
                    return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.BadRequest, null, "Only DRAFT or IN_PROGRESS stocktakes can be updated");

                header.StocktakeNumber = request.StocktakeNumber;
                header.StocktakeDate = request.StocktakeDate;
                header.BranchId = request.BranchId;
                header.Notes = request.Notes;
                header.CountedBy = request.CountedBy;
                header.UpdatedBy = userId;
                header.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return GetStocktakeById(id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StocktakeHeaderResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> DeleteStocktake(int id, int tenantId, int userId)
        {
            try
            {
                var header = _context.StocktakeHeaders.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId);
                if (header == null)
                    return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Stocktake not found");

                if (header.Status == "POSTED" || header.Status == "APPROVED")
                    return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Cannot delete an APPROVED or POSTED stocktake");

                header.IsDeleted = true;
                header.UpdatedBy = userId;
                header.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Stocktake deleted");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<StocktakeLineResponseVM> AddStocktakeLine(StocktakeLineRequestVM request, int tenantId, int userId)
        {
            try
            {
                var header = _context.StocktakeHeaders.FirstOrDefault(h => h.Id == request.StocktakeId && h.TenantId == tenantId);
                if (header == null)
                    return new ResponseResult<StocktakeLineResponseVM>(HttpStatusCode.NotFound, null, "Header not found");

                var line = new MStocktakeLine
                {
                    StocktakeId = request.StocktakeId,
                    ItemId = request.ItemId,
                    UomCode = "EA", // Default or from VM if added
                    SystemQuantity = request.SystemQuantity,
                    CountedQuantity = request.CountedQuantity,
                    VarianceNotes = request.VarianceNotes,
                    CountedBy = userId,
                    CountedAt = DateTime.UtcNow
                };

                _context.StocktakeLines.Add(line);
                _context.SaveChanges();

                var data = new StocktakeLineResponseVM
                {
                    Id = line.Id,
                    StocktakeId = line.StocktakeId,
                    ItemId = line.ItemId,
                    ItemName = _context.Items.Find(line.ItemId)?.Name,
                    UomCode = line.UomCode,
                    SystemQuantity = line.SystemQuantity,
                    CountedQuantity = line.CountedQuantity,
                    Variance = line.Variance,
                    VarianceNotes = line.VarianceNotes,
                    CountedBy = userId,
                    CountedAt = line.CountedAt
                };

                return new ResponseResult<StocktakeLineResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StocktakeLineResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<StocktakeLineResponseVM>> GetStocktakeLines(int headerId, int tenantId)
        {
            try
            {
                var lines = _context.StocktakeLines
                    .Include(x => x.Item)
                    .Where(x => x.StocktakeId == headerId)
                    .ToList();

                var data = lines.Select(l => new StocktakeLineResponseVM
                {
                    Id = l.Id,
                    StocktakeId = l.StocktakeId,
                    ItemId = l.ItemId,
                    ItemName = l.Item?.Name ?? "Unknown",
                    UomCode = l.UomCode,
                    SystemQuantity = l.SystemQuantity,
                    CountedQuantity = l.CountedQuantity,
                    Variance = l.Variance,
                    VarianceNotes = l.VarianceNotes,
                    CountedAt = l.CountedAt
                }).ToList();

                return new ResponseResult<List<StocktakeLineResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<StocktakeLineResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> CompleteStocktake(int id, int tenantId, int userId)
        {
            try
            {
                var header = _context.StocktakeHeaders.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId);
                if (header == null) return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Not found");

                header.Status = "COMPLETED";
                header.UpdatedBy = userId;
                header.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Stocktake marked as completed. Pending approval.");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> ApproveStocktake(ApproveStocktakeRequestVM request, int tenantId)
        {
            try
            {
                var header = _context.StocktakeHeaders.FirstOrDefault(x => x.Id == request.StocktakeId && x.TenantId == tenantId);
                if (header == null) return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Not found");

                header.Status = request.IsApproved ? "APPROVED" : "REJECTED";
                header.ApprovedBy = request.ApprovedBy;
                header.ApprovalDate = DateTime.UtcNow;
                header.UpdatedBy = request.ApprovedBy;
                header.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return new ResponseResult<bool>(HttpStatusCode.OK, true, $"Stocktake {header.Status}");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> PostStocktakeToInventory(int id, int tenantId, int userId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var header = _context.StocktakeHeaders
                    .Include(h => h.StocktakeLines)
                    .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId);

                if (header == null) return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Not found");
                if (header.Status != "APPROVED")
                    return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Only APPROVED stocktakes can be posted");

                foreach (var line in header.StocktakeLines)
                {
                    if (line.Variance != 0)
                    {
                        _transactionLog.LogTransaction(new StockTransactionLogRequestVM
                        {
                            ItemId = line.ItemId,
                            BranchId = header.BranchId,
                            TransactionType = "ADJUSTMENT",
                            QuantityChange = line.Variance,
                            ReferenceType = "STOCKTAKE",
                            ReferenceId = header.Id,
                            Notes = $"Stocktake Variance: {line.VarianceNotes}"
                        }, tenantId, userId);
                    }

                    var branchItem = _context.ItemBranch
                        .FirstOrDefault(ib => ib.ItemId == line.ItemId && ib.BranchId == header.BranchId && ib.TenantId == tenantId);

                    if (branchItem != null)
                    {
                        branchItem.LastStockCountDate = DateTime.UtcNow;
                    }
                }

                header.Status = "POSTED";
                header.UpdatedBy = userId;
                header.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                transaction.Commit();

                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Stocktake posted to inventory successfully");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<StocktakeLineResponseVM>> GetVarianceReport(int headerId, int tenantId)
        {
            var result = GetStocktakeLines(headerId, tenantId);
            if (result.StatusCode == HttpStatusCode.OK && result.Data != null)
            {
                result.Data = result.Data.Where(x => x.Variance != 0).ToList();
            }
            return result;
        }

        public ResponseResult<StocktakeSummaryVM> GetStocktakeSummary(int headerId, int tenantId)
        {
            try
            {
                var header = _context.StocktakeHeaders
                    .Include(h => h.StocktakeLines)
                    .FirstOrDefault(x => x.Id == headerId && x.TenantId == tenantId);

                if (header == null) return new ResponseResult<StocktakeSummaryVM>(HttpStatusCode.NotFound, null, "Not found");

                var summary = new StocktakeSummaryVM
                {
                    HeaderId = headerId,
                    TotalItemsCounted = header.StocktakeLines.Count,
                    TotalVarianceQty = header.StocktakeLines.Sum(l => l.Variance),
                    ItemsWithVariance = header.StocktakeLines.Count(l => l.Variance != 0),
                    Status = header.Status
                };

                return new ResponseResult<StocktakeSummaryVM>(HttpStatusCode.OK, summary);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StocktakeSummaryVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
