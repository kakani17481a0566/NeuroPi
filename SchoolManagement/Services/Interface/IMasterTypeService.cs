using SchoolManagement.ViewModel.Master;
using SchoolManagement.ViewModel.MasterType;

namespace SchoolManagement.Services.Interface
{
    //sai vardhan
    public interface IMasterTypeService
    {
        MasterTypeResponseVM GetById(int id);

        List<MasterTypeResponseVM> GetAll();

        List<MasterTypeResponseVM> GetAllByTenantId(int tenantId);
        MasterTypeResponseVM GetByIdAndTenantId(int id, int tenantId);

        MasterTypeResponseVM CreateMasterType(MasterTypeRequestVM masterType);

        MasterTypeResponseVM UpdateMasterType(int id, int tenantId, MasterTypeUpdateVM masterType);

        MasterTypeResponseVM DeleteById(int id, int tenantId);


    }
}
