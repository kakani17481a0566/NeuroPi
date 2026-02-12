using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.FormSubmission
{
    public class FormSubmissionResponseVM
    {
        public int Id { get; set; }

        public int FormId { get; set; }

        public int BranchId { get; set; }

        public int TargetUserId { get; set; }

        public int StatusId { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int SubmittedBy { get; set; }

        public float VersionId { get; set; }

        public int AppId { get; set; }

        public static FormSubmissionResponseVM ToViewModel(MFormSubmission submission)
        {
            return new FormSubmissionResponseVM
            {
                Id = submission.Id,
                FormId = submission.FormId,
                BranchId = submission.BranchId,
                TargetUserId = submission.TargetUserId,
                StatusId = submission.StatusId,
                EntryDate = submission.EntryDate,
                SubmissionDate = submission.SubmissionDate,
                SubmittedBy = submission.SubmittedBy,
                VersionId = submission.VersionId,
                AppId = submission.AppId,
            };
        }
        public static List<FormSubmissionResponseVM> ToViewModelList(List<MFormSubmission> formSubmissions)
        {
            return formSubmissions.Select(x => ToViewModel(x)).ToList();
        }
    }
}
