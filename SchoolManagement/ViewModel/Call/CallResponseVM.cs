using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.Call
{
    public class CallResponseVM
    {

        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Contact { get; set; }
        public int? StageId { get; set; }
        public string Stage { get; set; }
        public string AudioLink { get; set; }
        public string Remarks { get; set; }
        public TimeSpan? CallDuration { get; set; }
        public int TenantId { get; set; }

        public string TenantName { get; set; }
        public string BenificiaryName { get; set; }

        public string BeneficiaryRelationshipName { get; set; }
        public string CreatedByName { get; set; }

        public string? DirectionTypeName { get; set; }
        public string? CallStatusName { get; set; }

    }
}
