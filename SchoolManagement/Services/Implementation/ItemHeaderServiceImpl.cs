using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemHeader;

namespace SchoolManagement.Services.Implementation
{
    public class ItemHeaderServiceImpl : IItemHeaderService
    {
        private readonly SchoolManagementDb _context;
        public ItemHeaderServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public ItemHeaderVM CreateItemHeader(ItemHeaderRequestVM request)
        {
            throw new NotImplementedException();
        }

        public ItemHeaderVM DeleteItemHeader(int id)
        {
            throw new NotImplementedException();
        }

        public List<ItemHeaderResponseVM> GetAll()
        {
            throw new NotImplementedException();
        }

       
        

        public ItemHeaderVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ItemHeaderVM GetByIdAndTenantId(int id, int tenantId)
        {
            throw new NotImplementedException();
        }

        public ItemHeaderVM UpdateItemHeader(int id, ItemHeaderRequestVM request)
        {
            throw new NotImplementedException();
        }

        public List<ItemHeaderResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = (from ih in _context.ItemHeaders
                          join g in _context.generes on ih.GenreId equals g.Id
                          where ih.TenantId == tenantId && !ih.IsDeleted
                          select new ItemHeaderResponseVM
                          {
                              BookId = ih.Id,
                              CreatedOn = ih.CreatedOn,
                              Book = new Book
                              {
                                  title = ih.Title,
                                  coverImg = ""
                              },
                              Price = ih.Price,
                              Stock = 20,
                              Status = "available",
                              category = g.Name,
                              Author = ih.AuthorName,
                              PublisherAddress = new PublisherAddress
                              {
                                  street = "telanagana",
                                  line ="Hitex"
                              }
                          }).ToList();

            return result;
        }


    }
}
