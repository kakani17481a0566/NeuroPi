using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.FormSubmission
{
    public class FormSubmissionRequestVM
    {
        public int FormId { get; set; }

        public int BranchId { get; set; }

        public int SubmittedBy { get; set; }

        public int TargetUserId { get; set; }

        public int StatusId { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime SubmissionDate { get; set; }

        public float VersionId { get; set; }

        public int AppId { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public static MFormSubmission ToModel(FormSubmissionRequestVM requestVM)
        {
            return new MFormSubmission
            {
                FormId=requestVM.FormId,
                BranchId=requestVM.BranchId,
                SubmittedBy=requestVM.SubmittedBy,
                TargetUserId=requestVM.TargetUserId,
                StatusId=requestVM.StatusId,
                EntryDate=requestVM.EntryDate,
                SubmissionDate=requestVM.SubmissionDate,
                VersionId=requestVM.VersionId,
                AppId=requestVM.AppId,
                TenantId=requestVM.TenantId,
                CreatedBy=requestVM.createdBy,
                CreatedOn=DateTime.UtcNow,
            };
        }
    }
}
