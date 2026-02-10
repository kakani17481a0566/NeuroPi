using Microsoft.EntityFrameworkCore;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Data;
using NeuropiForms.Models;
using NeuropiForms.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NeuropiForms.Services.Impl
{
    public class FormServiceImpl : IFormService
    {
        private readonly NeuropiFormsDbContext _context;

        public FormServiceImpl(NeuropiFormsDbContext context)
        {
            _context = context;
        }

        public ResponseResult<IEnumerable<MForm>> GetAllForms()
        {
            var forms = _context.Forms.ToList();
            return new ResponseResult<IEnumerable<MForm>>(HttpStatusCode.OK, forms, "Forms retrieved successfully");
        }

        public ResponseResult<MForm?> GetFormById(int id)
        {
            var form = _context.Forms.Find(id);
            if (form == null)
            {
                return new ResponseResult<MForm?>(HttpStatusCode.NotFound, null, "Form not found");
            }
            return new ResponseResult<MForm?>(HttpStatusCode.OK, form, "Form retrieved successfully");
        }

        public ResponseResult<MForm> CreateForm(MForm form)
        {
            _context.Forms.Add(form);
            _context.SaveChanges();
            return new ResponseResult<MForm>(HttpStatusCode.Created, form, "Form created successfully");
        }

        public ResponseResult<MForm?> UpdateForm(int id, MForm form)
        {
            var existingForm = _context.Forms.Find(id);
            if (existingForm == null)
            {
                return new ResponseResult<MForm?>(HttpStatusCode.NotFound, null, "Form not found");
            }

            // Update properties
            existingForm.Title = form.Title;
            existingForm.Description = form.Description;
            existingForm.ActiveVersion = form.ActiveVersion;
            existingForm.IsActive = form.IsActive;
            existingForm.IsPublished = form.IsPublished;
            existingForm.VersionId = form.VersionId;
            existingForm.MaxVersions = form.MaxVersions;
            existingForm.ComplintensId = form.ComplintensId;
            existingForm.StorageTypeId = form.StorageTypeId;
            existingForm.StorageId = form.StorageId;
            existingForm.AppId = form.AppId;
            existingForm.TenantId = form.TenantId;
            
            existingForm.UpdatedOn = System.DateTime.UtcNow;
            existingForm.UpdatedBy = form.UpdatedBy;

            _context.SaveChanges();
            return new ResponseResult<MForm?>(HttpStatusCode.OK, existingForm, "Form updated successfully");
        }

        public ResponseResult<bool> DeleteForm(int id)
        {
            var form = _context.Forms.Find(id);
            if (form == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Form not found");
            }

            form.IsDeleted = true;
            form.UpdatedOn = System.DateTime.UtcNow;
            
            _context.SaveChanges();
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Form deleted successfully");
        }
    }
}
