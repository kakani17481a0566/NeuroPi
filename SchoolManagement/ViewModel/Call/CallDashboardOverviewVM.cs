using System;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.Call
{
    public class CallDashboardOverviewVM
    {
        public int TotalCalls { get; set; }
        public int InboundCalls { get; set; } // We can mock this or calculate based on some logic if needed
        public string TotalCallingDuration { get; set; }
        public string AvgHandleTime { get; set; }

        public List<CallVolumeVM> VolumeData { get; set; } = new List<CallVolumeVM>();
        public List<CallOutcomeVM> Outcomes { get; set; } = new List<CallOutcomeVM>();
        public List<CallResponseVM> RecentCalls { get; set; } = new List<CallResponseVM>();
        public List<DashboardTaskVM> Tasks { get; set; } = new List<DashboardTaskVM>();
        public List<DashboardLeadVM> HotLeads { get; set; } = new List<DashboardLeadVM>();
    }

    public class CallVolumeVM
    {
        public string Hour { get; set; }
        public int Inbound { get; set; }
        public int Outbound { get; set; }
        public int Missed { get; set; }
    }

    public class CallOutcomeVM
    {
        public string Label { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
    }

    public class DashboardTaskVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Due { get; set; }
        public string Priority { get; set; }
        public bool Done { get; set; }
    }

    public class DashboardLeadVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Score { get; set; }
        public string Avatar { get; set; }
        public string Status { get; set; }
    }
}
