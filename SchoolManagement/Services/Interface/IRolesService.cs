using SchoolManagement.ViewModel;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IRolesService
    {
        List<RolesResponseVM> GetAll(int tenantId);
        RolesResponseVM GetById(int id);
        RolesResponseVM Create(RolesRequestVM request);
        RolesResponseVM Update(int id, RolesUpdateVM request);
        bool Delete(int id);
    }
}
