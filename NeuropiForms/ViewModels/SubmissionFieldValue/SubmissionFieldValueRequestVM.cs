using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SubmissionFieldValue
{
    public class SubmissionFieldValueRequestVM
    {
        public int SubmissionId { get; set; }

        public int FieldId { get; set; }
        public string Value { get; set; }

        public DateTime ValueDate { get; set; }

        public string Remarks { get; set; }

        public int AppId { get; set; }  
        public int TenantId { get; set; }

        public static MSubmissionFieldValue ToModel(SubmissionFieldValueRequestVM requestVM)
        {
            return new MSubmissionFieldValue()
            {
                SubmissionId = requestVM.SubmissionId,
                FieldId = requestVM.FieldId,
                Value = requestVM.Value,
                ValueDate = requestVM.ValueDate,
                Remarks = requestVM.Remarks,
                AppId = requestVM.AppId,
                TenantId = requestVM.TenantId,
            };
        }
    }
}
