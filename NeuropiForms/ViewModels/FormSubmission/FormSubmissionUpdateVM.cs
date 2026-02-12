namespace NeuropiForms.ViewModels.FormSubmission
{
    public class FormSubmissionUpdateVM
    {
        public int FormId { get; set; }

        public int BranchId { get; set; }

        public int TargetUserId { get; set; }

        public int StatusId { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int SubmittedBy { get; set; }

        public float VersionId { get; set; }

        public int AppId { get; set; }
    }
}
