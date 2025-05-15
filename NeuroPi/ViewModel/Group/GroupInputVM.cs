public class GroupInputVM
{
    public string Name { get; set; }
    public int TenantId { get; set; }
  
    public int CreatedBy { get; set; }       // New Field (User ID who created the group)
}
