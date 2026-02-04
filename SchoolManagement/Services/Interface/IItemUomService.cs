using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IItemUomService
    {
        ResponseResult<ItemUomResponseVM> CreateUom(ItemUomRequestVM request, int tenantId, int userId);
        ResponseResult<ItemUomResponseVM> GetUomById(int id, int tenantId);
        ResponseResult<List<ItemUomResponseVM>> GetUomsByItemId(int itemId, int tenantId);
        ResponseResult<List<ItemUomResponseVM>> GetAllUoms(int tenantId);
        ResponseResult<ItemUomResponseVM> UpdateUom(int id, ItemUomRequestVM request, int tenantId, int userId);
        ResponseResult<bool> DeleteUom(int id, int tenantId, int userId);
        
        // Conversion Operations
        ResponseResult<decimal> ConvertQuantity(int itemId, string fromUom, string toUom, decimal quantity, int tenantId);
        ResponseResult<ItemUomResponseVM> GetBaseUom(int itemId, int tenantId);
    }
}
