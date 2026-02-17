using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SubmissionSectionValue;

namespace NeuropiForms.Services.Implementation
{
    public class SubmissionSectionValueServiceImpl : ISubmissionSectionValueService
    {
        private readonly NeuropiFormsDbContext _context;
        public SubmissionSectionValueServiceImpl(NeuropiFormsDbContext context)
        {
             _context=context;

        }
        public SubmissionSectionValueResponseVM AddSubmissionSection(SubmissionSectionValueRequestVM requestVM)
        {
            var submissionSectionValueModel=SubmissionSectionValueRequestVM.ToModel(requestVM);
            _context.Add(submissionSectionValueModel);
            _context.SaveChanges();
            return SubmissionSectionValueResponseVM.ToViewModel(submissionSectionValueModel);
        }

        public SubmissionSectionValueResponseVM DeleteSubmissionSectionValue(int id)
        {
            var result=_context.SubmissionSectionValues.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id==id);
            if (result != null)
            {
                result.IsDeleted=true;
                _context.SaveChanges();
                return SubmissionSectionValueResponseVM.ToViewModel(result);
            }
            return null;
        }

        public SubmissionSectionValueResponseVM GetSubmissionSectionById(int id)
        {
            var result=_context.SubmissionSectionValues.Where(s=>!s.IsDeleted).FirstOrDefault(s=>s.Id==id);
            if (result != null)
            {
                return SubmissionSectionValueResponseVM.ToViewModel(result);
            }
            return null;
        }

        public List<SubmissionSectionValueResponseVM> GetSubmissionSections()
        {
            var result=_context.SubmissionSectionValues.Where(S=>!S.IsDeleted).ToList();
            if (result != null && result.Count > 0)
            {
              return SubmissionSectionValueResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public SubmissionSectionValueResponseVM UpdateSubmissionSectionValue(int id, SubmissionSectionValueUpdateVM updateVM )
        {
            var result=_context.SubmissionSectionValues.Where(s=>!!s.IsDeleted).FirstOrDefault(s=>s.Id == id);
            if (result != null)
            {
                result.AppId = updateVM.AppId;
                result.SectionId = updateVM.SectionId;
                result.SubmissionId = updateVM.SubmissionId;
                result.Value = updateVM.Value;
                result.UpdatedBy=updateVM.TenantId;
                result.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return SubmissionSectionValueResponseVM.ToViewModel(result);
            }
            return null;
        }
    }
}
