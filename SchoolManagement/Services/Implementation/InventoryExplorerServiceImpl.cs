using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class InventoryExplorerServiceImpl : IInventoryExplorerService
    {
        private readonly SchoolManagementDb _context;

        public InventoryExplorerServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<List<InventoryItemVM>> GetInventoryList(int tenantId, int? branchId = null, int? categoryId = null, string? searchTerm = null)
        {
            try
            {
                var query = from item in _context.Items
                            join ib in _context.ItemBranch on item.Id equals ib.ItemId into itemStock
                            from stock in itemStock.DefaultIfEmpty()
                            where item.TenantId == tenantId && !item.IsDeleted
                            select new { item, stock };

                // Apply Filters
                if (branchId.HasValue)
                {
                    query = query.Where(x => x.stock != null && x.stock.BranchId == branchId.Value);
                }

                if (categoryId.HasValue)
                {
                    query = query.Where(x => x.item.CategoryId == categoryId.Value);
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(x => x.item.Name.Contains(searchTerm) || (x.item.ItemCode != null && x.item.ItemCode.Contains(searchTerm)));
                }

                var results = query.Select(x => new InventoryItemVM
                {
                    ItemId = x.item.Id,
                    ItemName = x.item.Name,
                    ItemCode = x.item.ItemCode ?? "N/A",
                    CategoryName = x.item.ItemCategory != null ? x.item.ItemCategory.Name : "General",
                    BranchId = x.stock != null ? x.stock.BranchId : 0,
                    BranchName = x.stock != null && x.stock.Branch != null ? x.stock.Branch.Name : "N/A",
                    CurrentQuantity = x.stock != null ? x.stock.ItemQuantity : 0,
                    BaseUom = x.item.BaseUom ?? "EA",
                    ReOrderLevel = x.stock != null ? x.stock.ItemReOrderLevel : 0,
                    AverageCost = x.stock != null ? (x.stock.AverageCost ?? x.stock.ItemCost) : 0,
                    TotalValue = x.stock != null ? x.stock.ItemQuantity * (x.stock.AverageCost ?? x.stock.ItemCost) : 0,
                    LastMovementDate = x.stock != null ? x.stock.LastMovementDate : null
                }).ToList();

                return new ResponseResult<List<InventoryItemVM>>(HttpStatusCode.OK, results);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<InventoryItemVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<InventoryItemVM> GetInventoryDetail(int itemId, int branchId, int tenantId)
        {
            try
            {
                var item = _context.Items
                    .Include(i => i.ItemCategory)
                    .FirstOrDefault(i => i.Id == itemId && i.TenantId == tenantId && !i.IsDeleted);

                if (item == null)
                    return new ResponseResult<InventoryItemVM>(HttpStatusCode.NotFound, null, "Item not found");

                var stock = _context.ItemBranch
                    .Include(ib => ib.Branch)
                    .FirstOrDefault(ib => ib.ItemId == itemId && ib.BranchId == branchId && ib.TenantId == tenantId && !ib.IsDeleted);

                var vm = new InventoryItemVM
                {
                    ItemId = item.Id,
                    ItemName = item.Name,
                    ItemCode = item.ItemCode ?? "N/A",
                    CategoryName = item.ItemCategory?.Name ?? "General",
                    BranchId = stock?.BranchId ?? branchId,
                    BranchName = stock?.Branch?.Name ?? "N/A",
                    CurrentQuantity = stock?.ItemQuantity ?? 0,
                    BaseUom = item.BaseUom ?? "EA",
                    ReOrderLevel = stock?.ItemReOrderLevel ?? 0,
                    AverageCost = stock != null ? (stock.AverageCost ?? stock.ItemCost) : 0,
                    TotalValue = stock != null ? stock.ItemQuantity * (stock.AverageCost ?? stock.ItemCost) : 0,
                    LastMovementDate = stock?.LastMovementDate
                };

                return new ResponseResult<InventoryItemVM>(HttpStatusCode.OK, vm);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InventoryItemVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
