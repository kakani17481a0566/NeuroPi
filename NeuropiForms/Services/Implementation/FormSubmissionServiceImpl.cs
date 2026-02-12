using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.FormSubmission;

namespace NeuropiForms.Services.Implementation
{
    public class FormSubmissionServiceImpl : IFormSubmissionService
    {
        public readonly NeuropiFormsDbContext _context;
        public FormSubmissionServiceImpl(NeuropiFormsDbContext context)
        {
            _context = context;
        }
        public List<FormSubmissionResponseVM> GetAll()
        {
            var result=_context.FormSubmissions.Where(f=>!f.IsDeleted).ToList();
            if (result != null && result.Count > 0)
            {
                
            }
            return null;
           
        }

        public FormSubmissionResponseVM AddFormSubmission(FormSubmissionRequestVM formSubmissionRequestVM)
        {
            var formSubmissionModel=FormSubmissionRequestVM.ToModel(formSubmissionRequestVM);
            _context.Add(formSubmissionModel);
            _context.SaveChanges();
            return new FormSubmissionResponseVM()
            {
                Id = formSubmissionModel.Id,
                AppId = formSubmissionModel.AppId,
                TargetUserId = formSubmissionModel.TargetUserId,
                BranchId = formSubmissionModel.BranchId,
                FormId = formSubmissionModel.FormId,
                StatusId = formSubmissionModel.StatusId,
                EntryDate = formSubmissionModel.EntryDate,
                SubmissionDate = formSubmissionModel.SubmissionDate,
                SubmittedBy = formSubmissionModel.SubmittedBy,
                VersionId = formSubmissionModel.VersionId,
            };

        }

        public FormSubmissionResponseVM GetFormSubmission(int id)
        {
            var result=_context.FormSubmissions.Where(f=>!f.IsDeleted).FirstOrDefault(f=>f.Id==id);
            if(result!=null)
            {
                return FormSubmissionResponseVM.ToViewModel(result);
            }
            return null;
        }

        public List<FormSubmissionResponseVM> GetAllFormSubmissions()
        {
            var result=_context.FormSubmissions.Where(f=>!f.IsDeleted).ToList();
            if(result!=null &&  result.Count>0)
            {
                return FormSubmissionResponseVM.ToViewModelList(result);
                
            }
            return null;
        }

        public FormSubmissionResponseVM DeleteFormSubmission(int id)
        {
            var result=_context.FormSubmissions.Where(f=>!f.IsDeleted).FirstOrDefault(f=>f.Id==id);
            if (result != null)
            {
                result.IsDeleted = true;
                _context.SaveChanges();
                return FormSubmissionResponseVM.ToViewModel(result);
            }
            return null;
        }

        public FormSubmissionResponseVM UpdateFormSubmission(int id, FormSubmissionUpdateVM formSubmissionUpdateVM)
        {
            var formSubmissionModel=_context.FormSubmissions.Where(f=>!f.IsDeleted).FirstOrDefault(f=>f.Id==id);
            if (formSubmissionModel != null)
            {
                formSubmissionModel.AppId = formSubmissionUpdateVM.AppId;
                formSubmissionModel.VersionId = formSubmissionUpdateVM.VersionId;
                formSubmissionModel.StatusId = formSubmissionUpdateVM.StatusId;
                formSubmissionModel.BranchId = formSubmissionUpdateVM.BranchId;
                formSubmissionModel.EntryDate = formSubmissionUpdateVM.EntryDate;
                formSubmissionModel.TargetUserId = formSubmissionUpdateVM.TargetUserId;
                formSubmissionModel.FormId = formSubmissionUpdateVM.FormId;
                formSubmissionModel.SubmissionDate = formSubmissionUpdateVM.SubmissionDate;
                formSubmissionModel.SubmittedBy = formSubmissionUpdateVM.SubmittedBy;
                _context.SaveChanges();
                return FormSubmissionResponseVM.ToViewModel(formSubmissionModel);
            }
            return null;
        }
    }
}
