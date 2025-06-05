using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Term
{
    public class TermRequestVM
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public MTerm ToModel()
        {
            return new MTerm
            {
                Name = this.Name,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
