using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Implementation
{
    public class StockAdjustmentReasonServiceImpl : IStockAdjustmentReasonService
    {
        private readonly SchoolManagementDb _context;

        public StockAdjustmentReasonServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<StockAdjustmentReasonResponseVM> CreateReason(StockAdjustmentReasonRequestVM request, int tenantId, int userId)
        {
            try
            {
                var reason = new MStockAdjustmentReason
                {
                    Code = request.Code.ToUpper(),
                    Description = request.Description,
                    AdjustmentType = request.AdjustmentType.ToUpper(),
                    RequiresApproval = request.RequiresApproval,
                    TenantId = tenantId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _context.StockAdjustmentReasons.Add(reason);
                _context.SaveChanges();

                return GetReasonById(reason.Id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StockAdjustmentReasonResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<StockAdjustmentReasonResponseVM> GetReasonById(int id, int tenantId)
        {
            try
            {
                var reason = _context.StockAdjustmentReasons
                    .FirstOrDefault(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

                if (reason == null)
                    return new ResponseResult<StockAdjustmentReasonResponseVM>(HttpStatusCode.NotFound, null, "Reason not found");

                var data = new StockAdjustmentReasonResponseVM
                {
                    Id = reason.Id,
                    Code = reason.Code,
                    Description = reason.Description,
                    AdjustmentType = reason.AdjustmentType,
                    RequiresApproval = reason.RequiresApproval,
                    CreatedOn = reason.CreatedOn
                };

                return new ResponseResult<StockAdjustmentReasonResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StockAdjustmentReasonResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<StockAdjustmentReasonResponseVM>> GetAllReasons(int tenantId, string? adjustmentType = null)
        {
            try
            {
                var query = _context.StockAdjustmentReasons
                    .Where(r => r.TenantId == tenantId && !r.IsDeleted);

                if (!string.IsNullOrEmpty(adjustmentType))
                    query = query.Where(r => r.AdjustmentType == adjustmentType.ToUpper());

                var data = query.OrderBy(r => r.Code)
                    .Select(r => new StockAdjustmentReasonResponseVM
                    {
                        Id = r.Id,
                        Code = r.Code,
                        Description = r.Description,
                        AdjustmentType = r.AdjustmentType,
                        RequiresApproval = r.RequiresApproval,
                        CreatedOn = r.CreatedOn
                    }).ToList();

                return new ResponseResult<List<StockAdjustmentReasonResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<StockAdjustmentReasonResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<StockAdjustmentReasonResponseVM> UpdateReason(int id, StockAdjustmentReasonRequestVM request, int tenantId, int userId)
        {
            try
            {
                var reason = _context.StockAdjustmentReasons.FirstOrDefault(r => r.Id == id && r.TenantId == tenantId);
                if (reason == null)
                    return new ResponseResult<StockAdjustmentReasonResponseVM>(HttpStatusCode.NotFound, null, "Reason not found");

                reason.Code = request.Code.ToUpper();
                reason.Description = request.Description;
                reason.AdjustmentType = request.AdjustmentType.ToUpper();
                reason.RequiresApproval = request.RequiresApproval;
                reason.UpdatedBy = userId;
                reason.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return GetReasonById(id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StockAdjustmentReasonResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> DeleteReason(int id, int tenantId, int userId)
        {
            try
            {
                var reason = _context.StockAdjustmentReasons.FirstOrDefault(r => r.Id == id && r.TenantId == tenantId);
                if (reason == null)
                    return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Reason not found");

                reason.IsDeleted = true;
                reason.UpdatedBy = userId;
                reason.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Reason deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error: {ex.Message}");
            }
        }
    }
}
