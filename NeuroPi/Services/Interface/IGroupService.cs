using NeuroPi.ViewModel.Group;
using System.Collections.Generic;

namespace NeuroPi.Services.Interface
{
    public interface IGroupService
    {
        List<GroupViewModel> GetAll();  // Get all groups
        GroupViewModel GetById(int id); // Get group by id
        GroupViewModel Create(GroupInputModel input); // Create a new group
        GroupViewModel Update(int id, GroupUpdateInputModel input); // Update an existing group
        bool Delete(int id); // Delete a group
    }
}
