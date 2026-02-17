using NeuropiForms.ViewModels.SubmissionSectionValue;

namespace NeuropiForms.Services.Interface
{
    public interface ISubmissionSectionValueService
    {

        SubmissionSectionValueResponseVM AddSubmissionSection(SubmissionSectionValueRequestVM requestVM);

        SubmissionSectionValueResponseVM GetSubmissionSectionById(int id);

        List<SubmissionSectionValueResponseVM> GetSubmissionSections();

        SubmissionSectionValueResponseVM DeleteSubmissionSectionValue(int id);

        SubmissionSectionValueResponseVM UpdateSubmissionSectionValue(int id,SubmissionSectionValueUpdateVM updateVM);
    }
}
