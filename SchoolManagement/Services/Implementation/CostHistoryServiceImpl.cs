using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Implementation
{
    public class CostHistoryServiceImpl : ICostHistoryService
    {
        private readonly SchoolManagementDb _context;

        public CostHistoryServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<CostHistoryResponseVM> RecordCost(CostHistoryRequestVM request, int tenantId, int userId)
        {
            try
            {
                var history = new MItemCostHistory
                {
                    ItemId = request.ItemId,
                    BranchId = request.BranchId,
                    EffectiveDate = request.EffectiveDate ?? DateTime.UtcNow,
                    CostType = request.CostType.ToUpper(), 
                    UnitCost = request.UnitCost,
                    SourceReferenceType = request.SourceReferenceType,
                    SourceReferenceId = request.SourceReferenceId,
                    TenantId = tenantId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _context.ItemCostHistory.Add(history);

                // Update Item Branch Cache
                var branchItem = _context.ItemBranch
                    .FirstOrDefault(ib => ib.ItemId == request.ItemId && ib.BranchId == request.BranchId && ib.TenantId == tenantId);

                if (branchItem != null)
                {
                    if (history.CostType == "PURCHASE" || history.CostType == "LAST")
                    {
                        branchItem.LastPurchaseCost = request.UnitCost;
                        branchItem.ItemCost = (int)request.UnitCost; 
                    }
                    else if (history.CostType == "AVERAGE")
                    {
                        branchItem.AverageCost = request.UnitCost;
                    }
                    
                    branchItem.UpdatedBy = userId;
                    branchItem.UpdatedOn = DateTime.UtcNow;
                }

                _context.SaveChanges();

                var data = new CostHistoryResponseVM
                {
                    Id = history.Id,
                    ItemId = history.ItemId,
                    ItemName = _context.Items.Find(history.ItemId)?.Name,
                    BranchId = history.BranchId,
                    BranchName = _context.Branches.Find(history.BranchId)?.Name,
                    EffectiveDate = history.EffectiveDate,
                    CostType = history.CostType,
                    UnitCost = history.UnitCost,
                    SourceReferenceType = history.SourceReferenceType,
                    SourceReferenceId = history.SourceReferenceId
                };

                return new ResponseResult<CostHistoryResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<CostHistoryResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<decimal> GetCurrentCost(int itemId, int? branchId, string costType, int tenantId)
        {
            try
            {
                var query = _context.ItemCostHistory
                    .Where(h => h.ItemId == itemId && h.CostType == costType.ToUpper() && h.TenantId == tenantId);

                if (branchId.HasValue)
                    query = query.Where(h => h.BranchId == branchId);

                var lastCost = query.OrderByDescending(h => h.EffectiveDate)
                    .ThenByDescending(h => h.Id)
                    .Select(h => h.UnitCost)
                    .FirstOrDefault();

                return new ResponseResult<decimal>(HttpStatusCode.OK, lastCost);
            }
            catch (Exception ex)
            {
                return new ResponseResult<decimal>(HttpStatusCode.InternalServerError, 0, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<CostHistoryResponseVM>> GetCostHistory(int itemId, int? branchId, int tenantId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                var query = _context.ItemCostHistory
                    .Include(h => h.Item)
                    .Include(h => h.Branch)
                    .Where(h => h.ItemId == itemId && h.TenantId == tenantId);

                if (branchId.HasValue)
                    query = query.Where(h => h.BranchId == branchId);

                if (fromDate.HasValue)
                    query = query.Where(h => h.EffectiveDate >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(h => h.EffectiveDate <= toDate.Value);

                var list = query.OrderByDescending(h => h.EffectiveDate)
                    .Select(h => new CostHistoryResponseVM
                    {
                        Id = h.Id,
                        ItemId = h.ItemId,
                        ItemName = h.Item.Name,
                        BranchId = h.BranchId,
                        BranchName = h.Branch.Name,
                        EffectiveDate = h.EffectiveDate,
                        CostType = h.CostType,
                        UnitCost = h.UnitCost,
                        SourceReferenceType = h.SourceReferenceType,
                        SourceReferenceId = h.SourceReferenceId
                    }).ToList();

                return new ResponseResult<List<CostHistoryResponseVM>>(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<CostHistoryResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<CostTrendVM>> GetCostTrends(int tenantId, int? branchId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                var query = _context.ItemCostHistory
                    .Include(h => h.Item)
                    .Where(h => h.TenantId == tenantId);

                if (branchId.HasValue)
                    query = query.Where(h => h.BranchId == branchId);

                if (fromDate.HasValue)
                    query = query.Where(h => h.EffectiveDate >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(h => h.EffectiveDate <= toDate.Value);

                var trends = query.GroupBy(h => new { h.ItemId, h.Item.Name, h.CostType })
                    .Select(g => new CostTrendVM
                    {
                        ItemId = g.Key.ItemId,
                        ItemName = g.Key.Name,
                        CurrentCost = g.OrderByDescending(x => x.EffectiveDate).Select(x => x.UnitCost).First(),
                        PreviousCost = g.OrderByDescending(x => x.EffectiveDate).Select(x => x.UnitCost).Skip(1).FirstOrDefault(),
                        EffectiveDate = g.OrderByDescending(x => x.EffectiveDate).Select(x => x.EffectiveDate).First()
                    }).ToList();

                foreach (var t in trends)
                {
                    if (t.PreviousCost > 0)
                    {
                        t.CostChange = t.CurrentCost - t.PreviousCost;
                        t.CostChangePercentage = (t.CostChange / t.PreviousCost) * 100;
                    }
                }

                return new ResponseResult<List<CostTrendVM>>(HttpStatusCode.OK, trends);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<CostTrendVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
