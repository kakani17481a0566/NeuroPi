using System.Collections.Generic;
using SchoolManagement.ViewModel.Term;

namespace SchoolManagement.Services.Interface
{
    public interface ITermService
    {
        List<TermResponseVM> GetAll();
        List<TermResponseVM> GetByTenantId(int tenantId);
        TermResponseVM GetById(int id);
        TermResponseVM GetById(int id, int tenantId);
        List<TermResponseVM> GetByAcademicYearId(int academicYearId);
        TermResponseVM Create(TermRequestVM termVM);
        TermResponseVM Update(int id, int tenantId, TermUpdateVM termVM);
        bool Delete(int id, int tenantId);
    }
}
