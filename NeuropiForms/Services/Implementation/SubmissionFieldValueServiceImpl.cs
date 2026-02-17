using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SubmissionFieldValue;

namespace NeuropiForms.Services.Implementation
{
    public class SubmissionFieldValueServiceImpl : ISubmissionFieldValueService
    {
        private readonly NeuropiFormsDbContext _context;
        public SubmissionFieldValueServiceImpl(NeuropiFormsDbContext context)
        {
            _context = context;
        }

        public SubmissionFieldValueResponseVM AddSubmissionFieldValue(SubmissionFieldValueRequestVM requestVM)
        {
            var modelSubmissionFieldValue=SubmissionFieldValueRequestVM.ToModel(requestVM);
            _context.Add(modelSubmissionFieldValue);
            _context.SaveChanges();
            return new SubmissionFieldValueResponseVM()
            {
                Id = modelSubmissionFieldValue.Id,
                SubmissionId = modelSubmissionFieldValue.SubmissionId,
                Value = modelSubmissionFieldValue.Value,
                AppId = modelSubmissionFieldValue.AppId,
                ValueDate = modelSubmissionFieldValue.ValueDate,
                TenantId = modelSubmissionFieldValue.TenantId,
                Remarks = modelSubmissionFieldValue.Remarks,
                FieldId = modelSubmissionFieldValue.FieldId,
            };
        }

        public SubmissionFieldValueResponseVM DeleteSubmissionValue(int id)
        {
            var result=_context.SubmissionFieldValues.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id == id);
            if (result != null)
            {
               result.IsDeleted = true;
                _context.SaveChanges();
                return SubmissionFieldValueResponseVM.ToViewModel(result);
            }
            return null;
        }

        public List<SubmissionFieldValueResponseVM> GetAllSubmissionFieldValues()
        {
            var result=_context.SubmissionFieldValues.Where(s=>!s.IsDeleted).ToList();
            if (result != null && result.Count() > 0)
            {
                return SubmissionFieldValueResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public SubmissionFieldValueResponseVM GetById(int id)
        {
            var result=_context.SubmissionFieldValues.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id==id);
            if (result != null)
            {
                return SubmissionFieldValueResponseVM.ToViewModel(result);
            }
            return null;
        }

        public SubmissionFieldValueResponseVM UpdateSubmissionFieldValue(int id, SubmissionFieldValueUpdateVM updateVM)
        {
            var result=_context.SubmissionFieldValues.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id==id);
            if (result != null)
            {
                result.SubmissionId = updateVM.SubmissionId;
                result.FieldId = updateVM.FieldId;
                result.AppId = updateVM.AppId;
                result.Value = updateVM.Value;
                result.ValueDate = updateVM.ValueDate;
                result.Remarks = updateVM.Remarks;
                result.UpdatedOn=DateTime.UtcNow;
                result.UpdatedBy = updateVM.TenantId;

                _context.SaveChanges();
                return SubmissionFieldValueResponseVM.ToViewModel(result) ;
            }
            return null;

        }
    }
}
