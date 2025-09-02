using SchoolManagement.ViewModel.Period;
using SchoolManagement.ViewModel.PhoneLog;

namespace SchoolManagement.Services.Interface
{
    public interface IPhoneLogService 
    {
        List<PhoneLogResponseVM>GetAll();
        List<PhoneLogResponseVM> GetByTenantId(int tenantId);
        List<PhoneLogResponseVM> GetBybranch_id(int id);
        PhoneLogResponseVM  create(PhoneLogRequestVM model);
        List<PhoneLogResponseVM> update(int branch_id, int tenant_id, PhoneLogUpdateVM model);
    }
}
