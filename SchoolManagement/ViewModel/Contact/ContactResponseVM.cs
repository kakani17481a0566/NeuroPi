using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Contact
{
    public class ContactResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PriNumber { get; set; } = string.Empty;
        public string? SecNumber { get; set; }
        public string? Email { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? State { get; set; }
        public string City { get; set; } = string.Empty;
        public string? Pincode { get; set; }

        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static ContactResponseVM ToViewModel(MContact contact)
        {
            return new ContactResponseVM
            {
                Id = contact.Id,
                Name = contact.Name,
                PriNumber = contact.PriNumber,
                SecNumber = contact.SecNumber,
                Email = contact.Email,
                Address1 = contact.Address1,
                Address2 = contact.Address2,
                State = contact.State,
                City = contact.City,
                Pincode = contact.Pincode,
                TenantId = contact.TenantId,
                CreatedBy = contact.CreatedBy,
                CreatedOn = contact.CreatedOn,
                UpdatedBy = contact.UpdatedBy,
                UpdatedOn = contact.UpdatedOn

            };
        }

        public static List<ContactResponseVM> ToViewModelList(List<MContact> contactList)
        {
            List<ContactResponseVM> result = new List<ContactResponseVM>();
            foreach (var contact in contactList)
            {
                result.Add(ToViewModel(contact));
            }
            return result;
        }
    }
}
