using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Items;
using System.Data;

namespace SchoolManagement.Services.Implementation
{
    public class ItemsServiceImpl : IItemsService
    {
        private readonly SchoolManagementDb _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _environment;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public ItemsServiceImpl(SchoolManagementDb context, Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        // 🔹 Create normal item
        public ItemsResponseVM CreateItems(ItemsRequestVM itemsRequestVM)
        {
            var newItem = itemsRequestVM.ToModel();
            newItem.CreatedOn = DateTime.UtcNow;
            newItem.IsDeleted = false;

            _context.Items.Add(newItem);
            _context.SaveChanges();

             // 🔹 Handle Image Upload
            if (itemsRequestVM.Image != null && itemsRequestVM.Image.Length > 0)
            {
                var imageUrl = UploadFile(itemsRequestVM.Image);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var itemImage = new MItemsImage
                    {
                        ItemId = newItem.Id,
                        Image = imageUrl,
                        TenantId = newItem.TenantId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = newItem.CreatedBy,
                        IsDeleted = false
                    };
                    _context.ItemsImages.Add(itemImage);
                    _context.SaveChanges();
                }
            }

            var resultVM = ItemsResponseVM.ToViewModel(newItem);
            
             // 🔹 Handle Image Upload
            if (itemsRequestVM.Image != null && itemsRequestVM.Image.Length > 0)
            {
                var imageUrl = UploadFile(itemsRequestVM.Image);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var itemImage = new MItemsImage
                    {
                        ItemId = newItem.Id,
                        Image = imageUrl,
                        TenantId = newItem.TenantId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = newItem.CreatedBy,
                        IsDeleted = false
                    };
                    _context.ItemsImages.Add(itemImage);
                    _context.SaveChanges();
                    
                    // Set image in response
                    resultVM.Image = imageUrl;
                }
            }

            return resultVM;
        }

        // 🔹 Soft delete
        public bool DeleteItemsByIdAndTenant(int id, int tenantId)
        {
            var item = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (item == null) return false;

            item.IsDeleted = true;
            item.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        // 🔹 Get all
        public List<ItemsResponseVM> GetAllItems()
        {
            return _context.Items
                .Where(i => !i.IsDeleted)
                .Select(i => ItemsResponseVM.ToViewModel(i))
                .ToList();
        }

        // 🔹 Get by Id
        public ItemsResponseVM GetItemsById(int id)
        {
            var item = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id);
            return item == null ? null : ItemsResponseVM.ToViewModel(item);
        }

        // 🔹 Get by Id + Tenant
        public ItemsResponseVM GetItemsByIdAndTenant(int id, int tenantId)
        {
            var item = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            return item == null ? null : ItemsResponseVM.ToViewModel(item);
        }

        // 🔹 Get by Tenant
        //public List<ItemsResponse> GetItemsByTenant(int tenantId)
        //{
        //    List<MItemBranch> items = _context.ItemBranch.Where(e => !e.IsDeleted & e.TenantId == tenantId).Include(e => e.Item).Include(e => e.Item.ItemCategory).ToList();
        //    List<ItemsResponse> result= new List<ItemsResponse>();
        //    foreach(var item in items)
        //    {
        //        var itemImage = _context.ItemsImages.Where(e => !e.IsDeleted && e.ItemId== item.Item.Id).FirstOrDefault();
        //        ItemsResponse itemsResponse = new ItemsResponse()
        //        {
        //            Id = item.ItemId,
        //            Name = item.Item.Name,
        //            categoryId = item.Item.CategoryId,
        //            categoryName = item.Item.ItemCategory.Name,
        //            size=item.Item.Height,
        //            price=item.ItemPrice,
        //            ItemQuantity = item.ItemQuantity,
        //            status =item.ItemQuantity!=null?"available":"not Available",
        //            Image=itemImage.Image
        //        };
        //        result.Add(itemsResponse);

        //    }
        //    return result;

        //}

        public ItemsFilterResponse GetItemsByTenant(int tenantId)
        {
            // ⚡ 1️⃣ Use projection directly in EF (avoid materializing full entities first)
            var itemsQuery = _context.ItemBranch
                .Where(e => !e.IsDeleted && e.TenantId == tenantId)
                .Select(e => new
                {
                    e.ItemId,
                    e.Item.Name,
                    e.Item.CategoryId,
                    CategoryName = e.Item.ItemCategory.Name,
                    e.Item.Height,
                    e.ItemPrice,
                    e.ItemQuantity,
                    Image = _context.ItemsImages
                        .Where(img => !img.IsDeleted && img.ItemId == e.Item.Id)
                        .Select(img => img.Image)
                        .FirstOrDefault()
                })
                .AsNoTracking(); // ✅ skip change tracking for read-only queries

            var items = itemsQuery.ToList();

            // ⚡ 2️⃣ Directly map to DTOs (no second DB roundtrip)
            var result = items.Select(i => new ItemsResponse
            {
                Id = i.ItemId,
                Name = i.Name,
                CategoryId = i.CategoryId,
                CategoryName = i.CategoryName,
                Size = i.Height,
                Price = i.ItemPrice,
                ItemQuantity = i.ItemQuantity,
                Status = i.ItemQuantity > 0 ? "Available" : "Not Available",
                Image = i.Image
            }).ToList();

            // ⚡ 3️⃣ Compute filters in-memory efficiently
            var filters = new ItemsFilters
            {
                Categories = result
                    .GroupBy(x => x.CategoryId)
                    .ToDictionary(g => g.Key, g => g.First().CategoryName),

                StatusList = new List<string> { "Available", "Not Available" },

                PriceList = result
                    .Select(x => x.Price)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList(),

                Sizes = result
                    .Where(x => x.Size > 0)
                    .Select(x => x.Size)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList()
            };

            return new ItemsFilterResponse
            {
                Items = result,
                Filters = filters
            };
        }




        // 🔹 Update
        public ItemsResponseVM UpdateItems(int id, int tenantId, ItemsUpdateVM vm)
        {
            var existing = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (existing == null) return null;

            existing.Name = vm.Name;
            existing.CategoryId = vm.CategoryId;
            existing.Height = vm.Height ?? existing.Height;
            existing.Width = vm.Width ?? existing.Width;
            existing.Depth = vm.Depth ?? existing.Depth;
            existing.Description = vm.Description;
            existing.ItemCode = vm.ItemCode;
            existing.IsGroup = vm.IsGroup;
            existing.UpdatedBy = vm.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            var resultVM = ItemsResponseVM.ToViewModel(existing);

            // 🔹 Handle Image Update
            if (vm.Image != null && vm.Image.Length > 0)
            {
                var imageUrl = UploadFile(vm.Image);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var existingImage = _context.ItemsImages.FirstOrDefault(img => !img.IsDeleted && img.ItemId == existing.Id);
                    if (existingImage != null)
                    {
                        existingImage.Image = imageUrl;
                        existingImage.UpdatedOn = DateTime.UtcNow; 
                    }
                    else
                    {
                        var itemImage = new MItemsImage
                        {
                            ItemId = existing.Id,
                            Image = imageUrl,
                            TenantId = existing.TenantId,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false
                        };
                        _context.ItemsImages.Add(itemImage);
                    }
                    _context.SaveChanges();
                    resultVM.Image = imageUrl;
                }
            }
            return resultVM;
        }

        // 🔹 Create item + group children
        public ItemsResponseVM CreateItemWithGroup(ItemInsertVM vm)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrWhiteSpace(vm.Name))
                    throw new ArgumentException("Item name is required.");
                if (vm.TenantId <= 0)
                    throw new ArgumentException("TenantId is required.");
                if (vm.CreatedBy <= 0)
                    throw new ArgumentException("CreatedBy (user id) is required.");

                var parent = new MItems
                {
                    Name = vm.Name,
                    CategoryId = vm.CategoryId,
                    TenantId = vm.TenantId,
                    Height = vm.Height ?? 0,
                    Width = vm.Width ?? 0,
                    Depth = vm.Depth ?? 0,
                    Description = vm.Description,
                    ItemCode = vm.ItemCode,
                    IsGroup = vm.IsGroup || vm.GroupItems.Any(),
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = vm.CreatedBy,
                    IsDeleted = false
                };

                _context.Items.Add(parent);
                _context.SaveChanges();

                if (parent.IsGroup && vm.GroupItems.Any())
                {
                    foreach (var child in vm.GroupItems)
                    {
                        var childExists = _context.Items.Any(i => i.Id == child.ItemId && !i.IsDeleted);
                        if (!childExists)
                            throw new ArgumentException($"Child item with Id {child.ItemId} does not exist.");

                        var groupRow = new MItemsGroup
                        {
                            SetItemId = parent.Id,
                            ItemId = child.ItemId,
                            Quantity = child.Quantity ?? 1,
                            FixedPrice = child.FixedPrice,
                            DiscountPrice = child.DiscountPrice,
                            TenantId = vm.TenantId,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = vm.CreatedBy,
                            IsDeleted = false
                        };
                        _context.ItemsGroup.Add(groupRow);
                    }
                    _context.SaveChanges();
                }

                transaction.Commit();
                return ItemsResponseVM.ToViewModel(parent);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // 🔹 Get item with group
        public ItemWithGroupResponseVM GetItemWithGroup(int id, int tenantId)
        {
            var parent = _context.Items.FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);
            if (parent == null) return null;

            var response = new ItemWithGroupResponseVM
            {
                Id = parent.Id,
                Name = parent.Name,
                Description = parent.Description,
                ItemCode = parent.ItemCode,
                CategoryId = parent.CategoryId,
                TenantId = parent.TenantId,
                IsGroup = parent.IsGroup
            };

            if (parent.IsGroup)
            {
                response.Children = (from ig in _context.ItemsGroup
                                     join c in _context.Items on ig.ItemId equals c.Id
                                     where ig.SetItemId == parent.Id
                                           && ig.TenantId == tenantId
                                           && !ig.IsDeleted
                                           && !c.IsDeleted
                                     select new GroupChildVM
                                     {
                                         ItemId = c.Id,
                                         Name = c.Name,
                                         Quantity = ig.Quantity,
                                         FixedPrice = ig.FixedPrice,
                                         DiscountPrice = ig.DiscountPrice
                                     }).ToList();
            }

            return response;
        }

        // 🔹 Helper to upload file
        private string UploadFile(Microsoft.AspNetCore.Http.IFormFile file)
        {
             try
            {
                if (file == null || file.Length == 0) return null;

                string connectionString = _configuration.GetSection("AzureBlobStorage:ConnectionString").Value;
                bool useDevelopmentStorage = connectionString == "UseDevelopmentStorage=true";

                if (useDevelopmentStorage)
                {
                    // Local Storage
                    var uploadsFolder = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Return relative path or full URL depending on need. Returning relative path assuming hosted correctly.
                    // Or constructing simplified URL.
                    // NOTE: Context.Request is not available in Service. We return a relative path or rely on hardcoded base if needed.
                    // For now, let's return "/uploads/filename"
                    return $"/uploads/{fileName}";
                }
                else
                {
                    // Azure Storage
                     string containerName = "student-docs"; // Reusing container or make configurable
                     if (string.IsNullOrEmpty(connectionString)) return null;

                     Azure.Storage.Blobs.BlobServiceClient blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
                     Azure.Storage.Blobs.BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                     containerClient.CreateIfNotExists();
                     // containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                     string blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                     Azure.Storage.Blobs.BlobClient blobClient = containerClient.GetBlobClient(blobName);

                     using (var stream = file.OpenReadStream())
                     {
                         blobClient.Upload(stream, true);
                     }
                     
                     // SAS or Public URL? GeneralController usage suggests SAS generation.
                     // Copying SAS logic.
                    var sasBuilder = new Azure.Storage.Sas.BlobSasBuilder
                    {
                        BlobContainerName = containerName,
                        BlobName = blobName,
                        Resource = "b",
                        ExpiresOn = DateTimeOffset.UtcNow.AddYears(100)
                    };
                    sasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);

                    Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                    return sasUri.ToString();
                }
            }
            catch (Exception ex)
            {
                // Log failure
                Console.WriteLine($"Upload failed: {ex.Message}");
                return null;
            }
        }
    }
}
