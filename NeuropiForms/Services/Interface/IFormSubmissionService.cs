using NeuropiForms.Services.Impl;
using NeuropiForms.ViewModels.FormSubmission;

namespace NeuropiForms.Services.Interface
{
    public interface IFormSubmissionService
    {
        List<FormSubmissionResponseVM> GetAll();

        FormSubmissionResponseVM AddFormSubmission(FormSubmissionRequestVM formSubmissionRequestVM);

        FormSubmissionResponseVM GetFormSubmission(int id);
        List<FormSubmissionResponseVM> GetAllFormSubmissions();
        FormSubmissionResponseVM DeleteFormSubmission(int id);

        FormSubmissionResponseVM UpdateFormSubmission(int id,FormSubmissionUpdateVM formSubmissionUpdateVM);
    }
}
