using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.LibraryBookTitle;

namespace SchoolManagement.Services.Implementation
{
    public class LibraryBookTitleServiceImpl : ILibraryBookTitleService
    {
        private readonly SchoolManagementDb _context;
        public LibraryBookTitleServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public LibraryBookTitleResponseVM CreateLibraryBookTitle(LibraryBookTitleRequestVM request)
        {
            var genre = _context.generes.FirstOrDefault(g => g.Name == request.Category && g.TenantId == request.TenantId);
            int? genreId = genre?.Id;

            // If Genre not found, create it? Or leave null. 
            // For simplicity, if not found and Category string exists, create new Genre.
            if (genreId == null && !string.IsNullOrEmpty(request.Category))
            {
                var newGenre = new Model.MGenere
                {
                    Name = request.Category,
                    TenantId = request.TenantId,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };
                _context.generes.Add(newGenre);
                _context.SaveChanges();
                genreId = newGenre.Id;
            }

            var libraryBookTitle = new Model.MLibraryBookTitle
            {
                Title = request.Title,
                AuthorName = request.Author,
                Price = request.Price,
                GenreId = genreId,
                TenantId = request.TenantId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = request.CategoryId // Optional if passed directly
            };

            _context.LibraryBookTitles.Add(libraryBookTitle);
            _context.SaveChanges();

            // Return mapped response
            return new LibraryBookTitleResponseVM
            {
                BookId = libraryBookTitle.Id,
                CreatedOn = libraryBookTitle.CreatedOn,
                Book = new Book { title = libraryBookTitle.Title, coverImg = "" },
                Price = libraryBookTitle.Price,
                Stock = request.Stock ?? 0, // Mocked persistence
                Status = request.Status ?? "available", // Mocked persistence
                category = request.Category,
                Author = libraryBookTitle.AuthorName,
                PublisherAddress = new PublisherAddress { street = "", line = "" }
            };
        }

        public bool DeleteLibraryBookTitle(int id)
        {
            var item = _context.LibraryBookTitles.Find(id);
            if (item == null) return false;

            item.IsDeleted = true;
            _context.LibraryBookTitles.Update(item);
            _context.SaveChanges();
            return true;
        }

        public List<LibraryBookTitleResponseVM> GetAll()
        {
            throw new NotImplementedException();
        }

       
        

        public LibraryBookTitleVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public LibraryBookTitleVM GetByIdAndTenantId(int id, int tenantId)
        {
            throw new NotImplementedException();
        }

        public LibraryBookTitleResponseVM UpdateLibraryBookTitle(int id, LibraryBookTitleRequestVM request)
        {
            var item = _context.LibraryBookTitles.Find(id);
            if (item == null) return null;

            item.Title = request.Title ?? item.Title;
            item.AuthorName = request.Author ?? item.AuthorName;
            item.Price = request.Price ?? item.Price;

            if (!string.IsNullOrEmpty(request.Category))
            {
                var genre = _context.generes.FirstOrDefault(g => g.Name == request.Category && g.TenantId == item.TenantId);
                if (genre != null)
                {
                    item.GenreId = genre.Id;
                }
                else
                {
                    // Create new genre if update specifies a new one
                    var newGenre = new Model.MGenere
                    {
                        Name = request.Category,
                        TenantId = item.TenantId,
                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    _context.generes.Add(newGenre);
                    _context.SaveChanges();
                    item.GenreId = newGenre.Id;
                }
            }
            
            _context.LibraryBookTitles.Update(item);
            _context.SaveChanges();

            return new LibraryBookTitleResponseVM
            {
                BookId = item.Id,
                CreatedOn = item.CreatedOn,
                Book = new Book { title = item.Title, coverImg = "" },
                Price = item.Price,
                Stock = request.Stock ?? 0,
                Status = request.Status ?? "available",
                category = request.Category,
                Author = item.AuthorName
            };
        }

        public List<LibraryBookTitleResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = (from ih in _context.LibraryBookTitles
                          join g in _context.generes on ih.GenreId equals g.Id
                          where ih.TenantId == tenantId && !ih.IsDeleted
                          select new LibraryBookTitleResponseVM
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
