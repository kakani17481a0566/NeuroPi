using System.ComponentModel.DataAnnotations.Schema;

public class GroupInputVM
{
    public string Name { get; set; }
    public int TenantId { get; set; }
  
    public int CreatedBy { get; set; }

    //[Column("created_on")]
    //public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

}
