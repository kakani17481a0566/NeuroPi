using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Utilites
{
    public class UtilitesRequestVM
    {
        public string Name { get; set; }

        public int TenantId { get; set; }

        public MUtilitiesList ToModel(UtilitesRequestVM request) =>
            new MUtilitiesList
            {
                Name = request.Name,
                TenantId = request.TenantId,
            };
            
    }
}
