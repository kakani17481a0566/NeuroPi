using System.Collections.Generic;
using SchoolManagement.ViewModel.TimeTable;

namespace SchoolManagement.Services.Interface
{
    public interface ITimeTableServices
    {
        List<TimeTableResponseVM> GetAll();
        List<TimeTableResponseVM> GetAll(int tenantId);
        TimeTableResponseVM GetById(int id);
        TimeTableResponseVM GetById(int id, int tenantId);
        TimeTableResponseVM Create(TimeTableRequestVM vm);
        TimeTableResponseVM Update(int id, int tenantId, TimeTableUpdateVM vm);
        bool Delete(int id, int tenantId);
    }
}
