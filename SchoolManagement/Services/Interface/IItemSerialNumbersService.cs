using SchoolManagement.ViewModel.Inventory;
using NeuroPi.UserManagment.Response;

namespace SchoolManagement.Services.Interface
{
    public interface IItemSerialNumbersService
    {
        ResponseResult<ItemSerialNumberResponseVM> CreateSerialNumber(ItemSerialNumberRequestVM request, int tenantId, int userId);
        ResponseResult<ItemSerialNumberResponseVM> UpdateSerialNumber(int id, ItemSerialNumberRequestVM request, int tenantId, int userId);
        ResponseResult<ItemSerialNumberResponseVM> GetSerialNumberById(int id, int tenantId);
        ResponseResult<List<ItemSerialNumberResponseVM>> GetSerialNumbersByItem(int itemId, int tenantId);
        ResponseResult<List<ItemSerialNumberResponseVM>> GetSerialNumbersByBatch(int batchId, int tenantId);
        ResponseResult<bool> DeleteSerialNumber(int id, int tenantId, int userId);
        ResponseResult<bool> ValidateSerialNumber(string serialNumber, int itemId, int tenantId);
    }
}
