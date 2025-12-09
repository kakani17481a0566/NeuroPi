using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.ViewModel.EnquiryForm;

namespace SchoolManagement.Services.Interface
{
    public interface IEnquiryFormService
    {
        // Create
        Task<EnquiryFormResponseVM> Create(EnquiryFormRequestVM request, int createdBy);

        // Get Single
        EnquiryFormResponseVM GetByUuid(Guid uuid, int tenantId);

        // Get All
        List<EnquiryFormResponseVM> GetAll(int tenantId);

        // Update (Optional — if needed later)
        public EnquiryFormResponseVM Update(Guid uuid, EnquiryFormUpdateVM request, int tenantId, int updatedBy);


        // Soft Delete
        bool Delete(Guid uuid, int tenantId, int deletedBy);
    }
}
