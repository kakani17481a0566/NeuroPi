using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SubmissionSectionValue
{
    public class SubmissionSectionValueResponseVM
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int SectionId { get; set; }
        public double Value { get; set; }
        public int AppId { get; set; }
        public int TenantId { get; set; }
        public static SubmissionSectionValueResponseVM ToViewModel(MSubmissionSectionValue modelVM)
        {
            return new SubmissionSectionValueResponseVM
            {
                Id = modelVM.Id,
                SubmissionId = modelVM.SubmissionId,
                SectionId = modelVM.SectionId,
                Value = modelVM.Value,
                AppId = modelVM.AppId,
                TenantId = modelVM.TenantId,
            };
        }
        public static List<SubmissionSectionValueResponseVM> ToViewModelList(List<MSubmissionSectionValue> modelVms)
        {
            return modelVms.Select(model => ToViewModel(model)).ToList();
        }

    }
}
