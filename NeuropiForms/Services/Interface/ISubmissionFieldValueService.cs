using NeuropiForms.ViewModels.SubmissionFieldValue;

namespace NeuropiForms.Services.Interface
{
    public interface ISubmissionFieldValueService
    {
        SubmissionFieldValueResponseVM GetById(int id);

        List<SubmissionFieldValueResponseVM> GetAllSubmissionFieldValues();

        SubmissionFieldValueResponseVM AddSubmissionFieldValue(SubmissionFieldValueRequestVM requestVM);

        SubmissionFieldValueResponseVM DeleteSubmissionValue(int id);
    }
}
