using SchoolManagement.ViewModel.VTimeTable;

public interface IMVTermTableService
{
    List<MVTermTableVM> GetVTermTableData(int tenantId, int courseId, int termId);
    //MVTermTableWeeklyMatrixVM GetTermTableWeeklyMatrix(int tenantId, int courseId, int termId);

    MVTermTableWeeklyMatrixVM GetTermTableWeeklyMatrix(int tenantId, int courseId, int termId);


}
