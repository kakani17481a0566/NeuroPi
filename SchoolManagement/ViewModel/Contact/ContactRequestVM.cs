using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Contact
{
    public class ContactRequestVM
    {
        public string Name { get; set; } = string.Empty;
        public string PriNumber { get; set; } = string.Empty;
        public string? SecNumber { get; set; }
        public string? Email { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? State { get; set; }
        public string City { get; set; } = string.Empty;
        public string? Pincode { get; set; }


        public static MContact ToModel(ContactRequestVM requestVM)
        {
            return new MContact()
            {
                Name = requestVM.Name,
                PriNumber = requestVM.PriNumber,
                SecNumber = requestVM.SecNumber,
                Email = requestVM.Email,
                Address1 = requestVM.Address1,
                Address2 = requestVM.Address2,
                State = requestVM.State,
                City = requestVM.City,
                Pincode = requestVM.Pincode
            };
        }
    }
}
