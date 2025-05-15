using NeuroPi.UserManagment.ViewModel.Group;
using System.Collections.Generic;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IGroupService
    {
        List<GroupVM> GetAll();
        GroupVM GetByGroupId(int groupId); 
        GroupVM GetByTenantAndGroupId(int tenantId, int groupId);
        GroupVM Create(GroupInputVM input);
        GroupVM Update(int groupId, GroupUpdateInputVM input);
        bool DeleteById(int groupId, int tenantId);

        List<GroupVM> GetByTenantId(int tenantId);
    }
}
