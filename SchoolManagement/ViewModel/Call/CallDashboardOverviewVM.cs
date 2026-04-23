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
}
