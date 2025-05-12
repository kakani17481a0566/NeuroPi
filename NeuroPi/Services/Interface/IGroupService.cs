using NeuroPi.UserManagment.ViewModel.Group;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IGroupService
    {
        List<GroupVM> GetAll();
        GroupVM GetById(int id);
        GroupVM Create(GroupInputVM input);
        GroupVM Update(int id, GroupUpdateInputVM input);
        bool Delete(int id);
    }
}
