namespace NeuroPi.UserManagment.ViewModel.Group
{
    public class GroupUpdateInputVM
    {
        public string Name { get; set; }  // Name is allowed to be updated
   
        public int? CreatedBy { get; set; }  // New Field (optional)
    }
}
