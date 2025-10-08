using SchoolManagement.ViewModel.PostalDeliveries;

namespace SchoolManagement.Services.Interface
{
    public interface IPostalDeliveriesService
    {

        List<PostalDeliveriesResponseVM> GetAll();

        PostalDeliveriesResponseVM CreatePostalDelivery(PostalDeliveriesRequestVM pdRequestVM);
    }
}
