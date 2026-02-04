using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Implementation
{
    public class ItemUomServiceImpl : IItemUomService
    {
        private readonly SchoolManagementDb _context;

        public ItemUomServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<ItemUomResponseVM> CreateUom(ItemUomRequestVM request, int tenantId, int userId)
        {
            try
            {
                if (request.IsBaseUom)
                {
                    // Reset other base UOMs for this item
                    var existingBase = _context.ItemUoms
                        .Where(u => u.ItemId == request.ItemId && u.TenantId == tenantId && u.IsBaseUom)
                        .ToList();
                    
                    foreach (var b in existingBase) b.IsBaseUom = false;
                }

                var uom = new MItemUom
                {
                    ItemId = request.ItemId,
                    UomCode = request.UomCode,
                    UomName = request.UomName,
                    ConversionFactor = request.ConversionFactor,
                    IsBaseUom = request.IsBaseUom,
                    Barcode = request.Barcode,
                    TenantId = tenantId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _context.ItemUoms.Add(uom);
                _context.SaveChanges();

                return GetUomById(uom.Id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<ItemUomResponseVM> GetUomById(int id, int tenantId)
        {
            try
            {
                var uom = _context.ItemUoms
                    .Include(u => u.Item)
                    .FirstOrDefault(u => u.Id == id && u.TenantId == tenantId && !u.IsDeleted);

                if (uom == null)
                    return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.NotFound, null, "UOM not found");

                var data = new ItemUomResponseVM
                {
                    Id = uom.Id,
                    ItemId = uom.ItemId,
                    ItemName = uom.Item?.Name ?? "Unknown",
                    UomCode = uom.UomCode,
                    UomName = uom.UomName,
                    ConversionFactor = uom.ConversionFactor,
                    IsBaseUom = uom.IsBaseUom,
                    Barcode = uom.Barcode,
                    CreatedOn = uom.CreatedOn
                };

                return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<ItemUomResponseVM>> GetUomsByItemId(int itemId, int tenantId)
        {
            try
            {
                var uoms = _context.ItemUoms
                    .Include(u => u.Item)
                    .Where(u => u.ItemId == itemId && u.TenantId == tenantId && !u.IsDeleted)
                    .ToList();

                var data = uoms.Select(u => new ItemUomResponseVM
                {
                    Id = u.Id,
                    ItemId = u.ItemId,
                    ItemName = u.Item?.Name ?? "Unknown",
                    UomCode = u.UomCode,
                    UomName = u.UomName,
                    ConversionFactor = u.ConversionFactor,
                    IsBaseUom = u.IsBaseUom,
                    Barcode = u.Barcode,
                    CreatedOn = u.CreatedOn
                }).ToList();

                return new ResponseResult<List<ItemUomResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<ItemUomResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<ItemUomResponseVM>> GetAllUoms(int tenantId)
        {
            try
            {
                var uoms = _context.ItemUoms
                    .Include(u => u.Item)
                    .Where(u => u.TenantId == tenantId && !u.IsDeleted)
                    .ToList();

                var data = uoms.Select(u => new ItemUomResponseVM
                {
                    Id = u.Id,
                    ItemId = u.ItemId,
                    ItemName = u.Item?.Name ?? "Unknown",
                    UomCode = u.UomCode,
                    UomName = u.UomName,
                    ConversionFactor = u.ConversionFactor,
                    IsBaseUom = u.IsBaseUom,
                    Barcode = u.Barcode,
                    CreatedOn = u.CreatedOn
                }).ToList();

                return new ResponseResult<List<ItemUomResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<ItemUomResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<ItemUomResponseVM> UpdateUom(int id, ItemUomRequestVM request, int tenantId, int userId)
        {
            try
            {
                var uom = _context.ItemUoms.FirstOrDefault(u => u.Id == id && u.TenantId == tenantId);
                if (uom == null)
                    return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.NotFound, null, "UOM not found");

                if (request.IsBaseUom && !uom.IsBaseUom)
                {
                    var existingBase = _context.ItemUoms
                        .Where(u => u.ItemId == request.ItemId && u.TenantId == tenantId && u.IsBaseUom)
                        .ToList();
                    foreach (var b in existingBase) b.IsBaseUom = false;
                }

                uom.UomCode = request.UomCode;
                uom.UomName = request.UomName;
                uom.ConversionFactor = request.ConversionFactor;
                uom.IsBaseUom = request.IsBaseUom;
                uom.Barcode = request.Barcode;
                uom.UpdatedBy = userId;
                uom.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return GetUomById(id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> DeleteUom(int id, int tenantId, int userId)
        {
            try
            {
                var uom = _context.ItemUoms.FirstOrDefault(u => u.Id == id && u.TenantId == tenantId);
                if (uom == null)
                    return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "UOM not found");

                uom.IsDeleted = true;
                uom.UpdatedBy = userId;
                uom.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "UOM deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<decimal> ConvertQuantity(int itemId, string fromUomCode, string toUomCode, decimal quantity, int tenantId)
        {
            try
            {
                var uoms = _context.ItemUoms
                    .Where(u => u.ItemId == itemId && u.TenantId == tenantId && !u.IsDeleted)
                    .ToList();

                var fromUom = uoms.FirstOrDefault(u => u.UomCode == fromUomCode);
                var toUom = uoms.FirstOrDefault(u => u.UomCode == toUomCode);

                if (fromUom == null || toUom == null)
                    return new ResponseResult<decimal>(HttpStatusCode.BadRequest, 0, "Invalid UOM for item");

                // Convert to base: qty * fromFactor
                decimal inBase = quantity * fromUom.ConversionFactor;
                // Convert to target: inBase / toFactor
                decimal result = inBase / toUom.ConversionFactor;

                return new ResponseResult<decimal>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<decimal>(HttpStatusCode.InternalServerError, 0, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<ItemUomResponseVM> GetBaseUom(int itemId, int tenantId)
        {
            try
            {
                var uom = _context.ItemUoms
                    .Include(u => u.Item)
                    .FirstOrDefault(u => u.ItemId == itemId && u.TenantId == tenantId && u.IsBaseUom && !u.IsDeleted);

                if (uom == null)
                    return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.NotFound, null, "Base UOM not found");

                var data = new ItemUomResponseVM
                {
                    Id = uom.Id,
                    ItemId = uom.ItemId,
                    ItemName = uom.Item?.Name,
                    UomCode = uom.UomCode,
                    UomName = uom.UomName,
                    ConversionFactor = uom.ConversionFactor,
                    IsBaseUom = true
                };

                return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ItemUomResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
