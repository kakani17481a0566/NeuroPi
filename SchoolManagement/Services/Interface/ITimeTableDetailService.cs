using System.Collections.Generic;
using SchoolManagement.ViewModel.TimeTableDetail;

namespace SchoolManagement.Services.Interface
{
    public interface ITimeTableDetailService
    {
        List<TimeTableDetailResponseVM> GetAll();
        List<TimeTableDetailResponseVM> GetAll(int tenantId);
        TimeTableDetailResponseVM GetById(int id);
        TimeTableDetailResponseVM GetById(int id, int tenantId);
        TimeTableDetailResponseVM Create(TimeTableDetailRequestVM vm);
        TimeTableDetailResponseVM Update(int id, int tenantId, TimeTableDetailUpdateVM vm);
        bool Delete(int id, int tenantId);
        TimeTableDetailTableResponseVM GetTableDetails(int tenantId);
        TimeTableDetailInsertOptionsVM GetInsertOptions(int tenantId);

      

        List<TimeTableDetailResponseByTTVM> GetByTimeTableId(int timeTableId, int tenantId);




    }
}
