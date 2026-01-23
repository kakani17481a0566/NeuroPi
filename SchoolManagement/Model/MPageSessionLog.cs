using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("page_session_log")]
    public class MPageSessionLog
    {
        [Key]
        [Column("id")]
        public long id { get; set; }

        [Column("user_id")]
        public int? user_id { get; set; }

        [Column("page_name")]
        public string? page_name { get; set; }

        [Column("page_open_time")]
        public DateTime page_open_time { get; set; } = DateTime.UtcNow;

        [Column("page_close_time")]
        public DateTime? page_close_time { get; set; }

        [Column("ip_address")]
        public string? ip_address { get; set; }

        [Column("tenant_id")]
        public int? tenant_id { get; set; }
    }
}
