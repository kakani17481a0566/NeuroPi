using SchoolManagement.ViewModel.TimeTableWorksheet;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITimeTableWorksheetService
    {
        // Get all worksheets (admin/global)
        List<TimeTableWorksheetResponseVM> GetAll();

        // Get all worksheets for a specific tenant
        List<TimeTableWorksheetResponseVM> GetAll(int tenantId);

        // Get worksheet by Id (admin/global)
        TimeTableWorksheetResponseVM GetById(int id);

        // Get worksheet by Id and tenantId
        TimeTableWorksheetResponseVM GetById(int id, int tenantId);

        // Create a new worksheet
        TimeTableWorksheetResponseVM Create(TimeTableWorksheetRequestVM request);

        // Update worksheet by Id and tenantId
        TimeTableWorksheetResponseVM Update(int id, int tenantId, TimeTableWorksheetUpdateVM request);

        // Soft delete worksheet by Id and tenantId
        bool Delete(int id, int tenantId);
    }
}
