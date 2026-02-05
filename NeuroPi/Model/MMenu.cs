using CloudinaryDotNet.Actions;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("menu")]
    public class MMenu
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("transkey")]
        public string TransKey { get; set; }
        [Column("icon")]
        public string Icon { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted {  get; set; }
        [Column("tenant_id")]
        public int TenantId {  get; set; }
        [Column("main_menu_id")]
        public int MainMenuId {  get; set; }
        [ForeignKey(nameof(MainMenuId))]
        public MMainMenu MainMenu { get; set; }

        public ICollection<MRolePermission> RolePermissions { get; set; }


    }
}
