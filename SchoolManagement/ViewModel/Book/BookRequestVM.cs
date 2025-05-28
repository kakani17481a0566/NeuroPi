using NeuroPi.UserManagment.Model;
using System;

namespace SchoolManagement.ViewModel.Book
{
    public class BookRequestVM
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
        public int? InstitutionId { get; set; }
        public int? BooksTypeId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public MBook ToModel()
        {
            return new MBook
            {
                Name = this.Name,
                ParentId = this.ParentId,
                UserId = this.UserId,
                InstitutionId = this.InstitutionId,
                BooksTypeId = this.BooksTypeId,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
