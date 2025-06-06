using SchoolManagement.ViewModel.VwComprehensiveTimeTables;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IVwComprehensiveTimeTablesService
    {
        List<VwComprehensiveTimeTableVM> GetAll();
    }
}
