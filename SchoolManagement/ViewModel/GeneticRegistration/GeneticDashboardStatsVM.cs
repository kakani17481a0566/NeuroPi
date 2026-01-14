
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticDashboardStatsVM
    {
        public int TotalRegistrations { get; set; }
        public int CompletedRegistrations { get; set; }
        public int DraftRegistrations { get; set; }
        public int DeletedRegistrations { get; set; }

        public List<LocationStatVM> TopLocations { get; set; }

        public List<GenderStatVM> GenderStats { get; set; }
        public int RegistrationsToday { get; set; }
        public int RegistrationsThisWeek { get; set; }
        public int RegistrationsThisMonth { get; set; }
    }

    public class MonthlyStatVM
    {
        public string Month { get; set; }
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Drafts { get; set; }
    }

    public class BranchStatVM
    {
        public string BranchName { get; set; }
        public int Count { get; set; }
    }

    public class LocationStatVM
    {
        public string Location { get; set; }
        public int Count { get; set; }
    }

    public class GenderStatVM
    {
        public string Gender { get; set; }
        public int Count { get; set; }
    }
}
