using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Call
{
    public class PermanentAddressVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }
        public string? ContactPerson { get; set; }

        public static PermanentAddressVM FromModel(MContact contact)
        {
            if (contact == null) return null;
            return new PermanentAddressVM
            {
                Id = contact.Id,
                Name = contact.Name,
                Address1 = contact.Address1,
                Address2 = contact.Address2,
                City = contact.City,
                State = contact.State,
                Pincode = contact.Pincode,
                ContactPerson = contact.ContactPerson
            };
        }
    }
}
