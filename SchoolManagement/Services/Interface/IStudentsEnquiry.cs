using SchoolManagement.ViewModel.StudentsEnquiry;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentsEnquiry
    {
        int CreateStudentEnquiry(StudentEnquiryRequestDataVM vm);

        List<StudentEnquiryResponseVM> GetAllStudentEnquiries();

        List<StudentEnquiryResponseVM> GetStudentEnquiriesByTenant(int tenantId);

        StudentEnquiryResponseVM GetStudentEnquiryById(int id);
        StudentEnquiryResponseVM GetStudentEnquiryByIdAndTenant(int id, int tenantId);
        bool DeleteStudentEnquiryByIdAndTenant(int id, int tenantId);
        StudentEnquiryResponseVM UpdateStudentEnquiry(int id, int tenantId, StudentEnquiryUpdateVM vm);
    }
}
