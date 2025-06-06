﻿
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.Services.Interface
{
    //saivardhan
    public interface IMasterService
    {
        MasterResponseVM GetById(int id);

        List<MasterResponseVM> GetAll();

        List<MasterResponseVM> GetAllByTenantId(int tenantId);
        MasterResponseVM GetByIdAndTenantId(int id, int tenantId);

        MasterResponseVM CreateMasterType(MasterRequestVM masterType);


        MasterResponseVM UpdateMasterType(int id, int tenantId, MasterUpdateVM masterType);

        MasterResponseVM DeleteById(int id, int tenantId);

        List<MasterResponseVM> GetAllByMasterTypeId(int id,int tenantId);

    }
}
