using System;

namespace SchoolManagement.ViewModel.PageSession
{
    public class PageSessionLogDto
    {
        public long Id { get; set; }
        public string? IpAddress { get; set; }
        public string? PageName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime PageOpenTime { get; set; }
        public DateTime? PageCloseTime { get; set; }
        public double? TimeSpentSeconds { get; set; }
    }
}
