using NeuropiForms.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuropiForms.ViewModels.SubmissionSectionValue
{
    public class SubmissionSectionValueRequestVM
    {
        public int SubmissionId { get; set; }
        public int SectionId { get; set; }
        public double Value { get; set; }
        public int AppId { get; set; }
        public int TenantId { get; set; }

        public static MSubmissionSectionValue ToModel(SubmissionSectionValueRequestVM vm)
        {
            return new MSubmissionSectionValue
            {
                SubmissionId = vm.SubmissionId,
                SectionId = vm.SectionId,
                Value = vm.Value,
                AppId = vm.AppId,
                TenantId = vm.TenantId,
                CreatedBy=vm.TenantId,
                CreatedOn=DateTime.UtcNow,
            };
        }
    }
}
