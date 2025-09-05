using SchoolManagement.ViewModel.StudentRegistration;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentRegistration
    {
        StudentRegistrationResponseVM Create(StudentRegistrationRequestVM request);

    }
}
