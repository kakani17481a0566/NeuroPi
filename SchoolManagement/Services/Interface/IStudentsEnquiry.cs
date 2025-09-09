using SchoolManagement.ViewModel.StudentsEnquiry;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentsEnquiry
    {
        long CreateStudentEnquiry(StudentEnquiryRequestDataVM vm);

        List<StudentEnquiryResponseVM> GetAllStudentEnquiries();

        List<StudentEnquiryResponseVM> GetStudentEnquiriesByTenant(int tenantId);

        StudentEnquiryResponseVM GetStudentEnquiryById(long id);
        StudentEnquiryResponseVM GetStudentEnquiryByIdAndTenant(long id, int tenantId);
        bool DeleteStudentEnquiryByIdAndTenant(long id, int tenantId);
        StudentEnquiryResponseVM UpdateStudentEnquiry(long id, int tenantId, StudentEnquiryUpdateVM vm);

    }
}
