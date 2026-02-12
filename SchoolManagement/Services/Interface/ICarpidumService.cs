using SchoolManagement.ViewModel.Carpidum;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICarpidumService
    {
        List<CarpidumVM> GetAll(int tenantId);
        CarpidumVM? GetById(int id, int tenantId);
        List<CarpidumVM> GetByStudentId(int studentId, int tenantId);
        CarpidumVM? Create(CarpidumRequestVM request, out string message);
        CarpidumVM? Update(int id, CarpidumRequestVM request, out string message);
        bool Delete(int id, int tenantId);
    }
}
