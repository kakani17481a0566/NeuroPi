namespace NeuropiForms.ViewModels.SubmissionFieldValue
{
    public class SubmissionFieldValueUpdateVM
    {

        public int SubmissionId { get; set; }

        public int FieldId { get; set; }
        public string Value { get; set; }

        public DateTime ValueDate { get; set; }

        public string Remarks { get; set; }

        public int AppId { get; set; }

        public int TenantId {  get; set; }


    }
}
