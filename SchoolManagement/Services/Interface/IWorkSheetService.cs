using SchoolManagement.ViewModel.Worksheets;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IWorkSheetService
    {
        // Get all worksheets (admin/global)
        List<WorksheetResponseVM> GetAll();

        // Get all worksheets for a specific tenant
        List<WorksheetResponseVM> GetAll(int tenantId);

        // Get worksheet by Id (admin/global)
        WorksheetResponseVM GetById(int id);

        // Get worksheet by Id and tenantId
        WorksheetResponseVM GetById(int id, int tenantId);

        // Create a new worksheet
        WorksheetResponseVM Create(WorksheetRequestVM request);

        // Update worksheet by Id and tenantId
        WorksheetResponseVM Update(int id, int tenantId, WorksheetUpdateVM request);

        // Soft delete worksheet by Id and tenantId
        bool Delete(int id, int tenantId);
    }
}
