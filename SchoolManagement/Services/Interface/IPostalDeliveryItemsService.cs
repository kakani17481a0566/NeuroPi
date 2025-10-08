using SchoolManagement.ViewModel.PostalDeliveryItems;

namespace SchoolManagement.Services.Interface
{
    public interface IPostalDeliveryItemsService
    {

        List<PostalDeliveryItemsResponseVM> GetAll();

        string CreatepostalDeliveryItems(PostalDeliveryItemsRequestVM pDIRequestVM);
    }
}
