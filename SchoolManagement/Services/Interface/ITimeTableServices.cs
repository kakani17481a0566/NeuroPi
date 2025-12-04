using NeuroPi.UserManagment.Response;
using SchoolManagement.Model;
using SchoolManagement.ViewModel.TableFile;
using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.VTimeTable;
using System.Collections.Generic;
using static SchoolManagement.ViewModel.TableFile.TableFileResponse;

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
        WeekTimeTableData GetWeeklyTimeTable(int weekId, int tenantId, int courseId);
        bool Delete(int id, int tenantId);
        
        List<TimeTableFullResponseVM> GetWeekTimeTable(int weekId, int courseId, int tenantId);


        TimeTableData GetAllStructured(int tenantId);
        TimeTableInsertTableOptionsVM GetInsertOptions(int tenantId);

        


    }
}
