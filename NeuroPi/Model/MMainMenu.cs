using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("main_menu")]
    public class MMainMenu
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("transkey")]
        public string Transkey { get; set; }
        [Column("icon")]
        public string Icon { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public ICollection<MMenu> Menus { get; set; }
    }
}
