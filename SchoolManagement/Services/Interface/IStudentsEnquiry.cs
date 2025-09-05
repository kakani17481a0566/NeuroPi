using SchoolManagement.ViewModel.StudentsEnquiry;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentsEnquiry
    {
        long CreateStudentEnquiry(StudentEnquiryRequestDataVM vm);
    }
}
