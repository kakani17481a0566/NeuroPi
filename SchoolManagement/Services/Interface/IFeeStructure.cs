using SchoolManagement.ViewModel.FeeStructure;
using SchoolManagement.Response;

namespace SchoolManagement.Services.Interface
{
    public interface IFeeStructure
    {
        // Create a new fee structure
        ResponseResult<int> CreateFeeStructure(FeeStructureRequestVM vm);

        // Get fee structure by Id + tenant
        ResponseResult<FeeStructureResponseVM> GetFeeStructureById(int id, int tenantId);

        // Get all fee structures for a tenant
        ResponseResult<List<FeeStructureResponseVM>> GetAllFeeStructures(int tenantId);

        // Update fee structure
        ResponseResult<bool> UpdateFeeStructure(int id, FeeStructureRequestVM vm);

        // Soft delete fee structure
        ResponseResult<bool> DeleteFeeStructure(int id, int tenantId);
    }
}
