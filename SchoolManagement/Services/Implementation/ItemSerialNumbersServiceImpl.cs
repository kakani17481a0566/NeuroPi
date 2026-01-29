using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class ItemSerialNumbersServiceImpl : IItemSerialNumbersService
    {
        private readonly SchoolManagementDb _context;

        public ItemSerialNumbersServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<ItemSerialNumberResponseVM> CreateSerialNumber(ItemSerialNumberRequestVM request, int tenantId, int userId)
        {
            // Check for duplicate
            if (_context.Set<MItemSerialNumber>().Any(s => s.ItemId == request.ItemId && s.SerialNumber == request.SerialNumber && s.TenantId == tenantId && !s.IsDeleted))
            {
                 return new ResponseResult<ItemSerialNumberResponseVM>(HttpStatusCode.Conflict, null, "Serial number already exists for this item");
            }

            var serial = new MItemSerialNumber
            {
                ItemId = request.ItemId,
                SerialNumber = request.SerialNumber,
                BatchId = request.BatchId,
                BranchId = request.BranchId,
                Status = request.Status,
                ReceivedDate = request.ReceivedDate ?? DateTime.UtcNow,
                WarrantyExpiryDate = request.WarrantyExpiryDate,
                TenantId = tenantId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Set<MItemSerialNumber>().Add(serial);
            _context.SaveChanges();

            return new ResponseResult<ItemSerialNumberResponseVM>(HttpStatusCode.Created, MapToResponse(serial), "Serial number created successfully");
        }

        public ResponseResult<ItemSerialNumberResponseVM> UpdateSerialNumber(int id, ItemSerialNumberRequestVM request, int tenantId, int userId)
        {
            var serial = _context.Set<MItemSerialNumber>().FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (serial == null)
            {
                return new ResponseResult<ItemSerialNumberResponseVM>(HttpStatusCode.NotFound, null, "Serial number not found");
            }

            serial.SerialNumber = request.SerialNumber;
            serial.BatchId = request.BatchId;
            serial.BranchId = request.BranchId;
            serial.Status = request.Status;
            serial.ReceivedDate = request.ReceivedDate;
            serial.WarrantyExpiryDate = request.WarrantyExpiryDate;
            serial.UpdatedBy = userId;
            serial.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return new ResponseResult<ItemSerialNumberResponseVM>(HttpStatusCode.OK, MapToResponse(serial), "Serial number updated successfully");
        }

        public ResponseResult<ItemSerialNumberResponseVM> GetSerialNumberById(int id, int tenantId)
        {
            var serial = _context.Set<MItemSerialNumber>()
                .Include(s => s.Item)
                .Include(s => s.Batch)
                .Include(s => s.Branch)
                .FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);

            if (serial == null)
            {
                return new ResponseResult<ItemSerialNumberResponseVM>(HttpStatusCode.NotFound, null, "Serial number not found");
            }

            return new ResponseResult<ItemSerialNumberResponseVM>(HttpStatusCode.OK, MapToResponse(serial), "Serial number retrieved successfully");
        }

        public ResponseResult<List<ItemSerialNumberResponseVM>> GetSerialNumbersByItem(int itemId, int tenantId)
        {
             var serials = _context.Set<MItemSerialNumber>()
                .Include(s => s.Item)
                .Include(s => s.Batch)
                .Include(s => s.Branch)
                .Where(s => s.ItemId == itemId && s.TenantId == tenantId && !s.IsDeleted)
                .ToList();

            var response = serials.Select(MapToResponse).ToList();
            return new ResponseResult<List<ItemSerialNumberResponseVM>>(HttpStatusCode.OK, response, "Serial numbers retrieved successfully");
        }

        public ResponseResult<List<ItemSerialNumberResponseVM>> GetSerialNumbersByBatch(int batchId, int tenantId)
        {
             var serials = _context.Set<MItemSerialNumber>()
                .Include(s => s.Item)
                .Include(s => s.Batch)
                .Include(s => s.Branch)
                .Where(s => s.BatchId == batchId && s.TenantId == tenantId && !s.IsDeleted)
                .ToList();

            var response = serials.Select(MapToResponse).ToList();
            return new ResponseResult<List<ItemSerialNumberResponseVM>>(HttpStatusCode.OK, response, "Serial numbers retrieved successfully");
        }

        public ResponseResult<bool> DeleteSerialNumber(int id, int tenantId, int userId)
        {
             var serial = _context.Set<MItemSerialNumber>().FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (serial == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Serial number not found");
            }

            serial.IsDeleted = true;
            serial.UpdatedBy = userId;
            serial.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Serial number deleted successfully");
        }

        public ResponseResult<bool> ValidateSerialNumber(string serialNumber, int itemId, int tenantId)
        {
            var exists = _context.Set<MItemSerialNumber>().Any(s => s.SerialNumber == serialNumber && s.ItemId == itemId && s.TenantId == tenantId && !s.IsDeleted);
            return new ResponseResult<bool>(HttpStatusCode.OK, exists, exists ? "Serial number exists" : "Serial number available");
        }

        private ItemSerialNumberResponseVM MapToResponse(MItemSerialNumber serial)
        {
            return new ItemSerialNumberResponseVM
            {
                Id = serial.Id,
                ItemId = serial.ItemId,
                ItemName = serial.Item?.Name ?? "",
                SerialNumber = serial.SerialNumber,
                BatchId = serial.BatchId,
                BatchNumber = serial.Batch?.BatchNumber,
                BranchId = serial.BranchId,
                BranchName = serial.Branch?.Name,
                Status = serial.Status,
                ReceivedDate = serial.ReceivedDate,
                SoldDate = serial.SoldDate,
                WarrantyExpiryDate = serial.WarrantyExpiryDate,
                CreatedOn = serial.CreatedOn
            };
        }
    }
}
