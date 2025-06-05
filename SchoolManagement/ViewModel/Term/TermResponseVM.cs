using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Term
{
    public class TermResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TenantId { get; set; }

        public static TermResponseVM FromModel(MTerm term)
        {
            if (term == null) return null;
            return new TermResponseVM
            {
                Id = term.Id,
                Name = term.Name,
                StartDate = term.StartDate,
                EndDate = term.EndDate,
                TenantId = term.TenantId
            };
        }
    }
}
