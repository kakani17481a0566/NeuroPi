using SchoolManagement.ViewModel.WorkSheets;

namespace SchoolManagement.Services.Interface
{
    public interface IWorkSheetService
    {
        List<WorkSheetResponseVM> GetAllWorkSheets();
        List<WorkSheetResponseVM> GetWorkSheetsByTenantId(int tenantId);
        WorkSheetResponseVM GetWorkSheetsById(int Id);

        WorkSheetResponseVM GetWorkSheetByTenantIdAndId(int tenantId, int id);

        WorkSheetResponseVM CreateWorkSheet(WorkSheetRequestVM workSheet);

        WorkSheetResponseVM UpdateWorkSheet(int id, int tenantId, WorkSheetUpdateVM workSheet);

        bool DeleteWorkSheetByIdAndTenantId(int id, int tenantId);

    }
}
