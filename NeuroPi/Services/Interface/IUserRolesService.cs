using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserRolesService
    {
        List<MUserRole> GetAll();
        MUserRole GetById(int id);
        MUserRole Create(MUserRole userRole);
        MUserRole Update(int id, MUserRole userRole);
        bool Delete(int id);
    }
}
