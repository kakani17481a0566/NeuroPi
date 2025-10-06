using System.Collections.Generic;
using SchoolManagement.ViewModel.Corporate;

namespace SchoolManagement.Services.Interface
{
    public interface ICorporateService
    {
        List<CorporateVM> GetAll(int tenantId);
    }
}
