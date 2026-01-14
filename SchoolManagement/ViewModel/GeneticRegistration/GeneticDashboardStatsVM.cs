
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticDashboardStatsVM
    {
        public int TotalRegistrations { get; set; }
        public int CompletedRegistrations { get; set; }
        public int DraftRegistrations { get; set; }
        public int DeletedRegistrations { get; set; }

        public List<MonthlyStatVM> MonthlyStats { get; set; }
    }

    public class MonthlyStatVM
    {
        public string Month { get; set; }
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Drafts { get; set; }
    }
}
