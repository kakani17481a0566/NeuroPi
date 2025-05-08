using NeuroPi.ViewModel.Group;
using System.Collections.Generic;

namespace NeuroPi.Services.Interface
{
    public interface IGroupService
    {
        List<GroupViewModel> GetAll(); 
        GroupViewModel GetById(int id); 
        GroupViewModel Create(GroupInputModel input); 
        GroupViewModel Update(int id, GroupUpdateInputModel input); 
        bool Delete(int id);
    }
}
