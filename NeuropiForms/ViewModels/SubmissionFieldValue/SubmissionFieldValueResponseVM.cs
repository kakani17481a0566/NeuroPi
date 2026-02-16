using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SubmissionFieldValue
{
    public class SubmissionFieldValueResponseVM
    {
        public int Id { get; set; }

        public int SubmissionId { get; set; }

        public int FieldId { get; set; }
        public string Value { get; set; }

        public DateTime ValueDate { get; set; }

        public string Remarks { get; set; }

        public int AppId { get; set; }
        public int TenantId { get; set; }

        public static SubmissionFieldValueResponseVM ToViewModel(MSubmissionFieldValue mSubmissionFieldValue)
        {
            return new SubmissionFieldValueResponseVM
            {
                Id = mSubmissionFieldValue.Id,
                SubmissionId = mSubmissionFieldValue.SubmissionId,
                FieldId = mSubmissionFieldValue.FieldId,
                Value = mSubmissionFieldValue.Value,
                ValueDate = mSubmissionFieldValue.ValueDate,
                Remarks = mSubmissionFieldValue.Remarks,
                AppId = mSubmissionFieldValue.AppId,
                TenantId = mSubmissionFieldValue.TenantId,
            };
        }
        public static List<SubmissionFieldValueResponseVM> ToViewModelList(List<MSubmissionFieldValue> mSubmissionFieldValues)
        {
            return mSubmissionFieldValues.Select(m => ToViewModel(m)).ToList();
        }

    }
}
