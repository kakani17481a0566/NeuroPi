using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionResponses;

namespace SchoolManagement.Services
{
    public class QuestionResponsesService : IQuestionResponsesService
    {
        private readonly SchoolManagementDb _context;

        public QuestionResponsesService(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<QuestionResponsesListVM> GetAllByTenantId(int tenantId)
        {
            var data = _context.QuestionResponses
                .Where(r => r.TenantId == tenantId && !r.IsDeleted)
                .ToList();

            return QuestionResponsesListVM.ToViewModelList(data);
        }

        public QuestionResponsesGetVM GetByIdAndTenantId(int id, int tenantId)
        {
            var response = _context.QuestionResponses
                .FirstOrDefault(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

            return QuestionResponsesGetVM.ToViewModel(response);
        }

        public QuestionResponsesGetVM Create(QuestionResponsesCreateVM request)
        {
            var model = QuestionResponsesCreateVM.ToModel(request);
            model.CreatedOn = DateTime.UtcNow;
            _context.QuestionResponses.Add(model);
            _context.SaveChanges();

            return QuestionResponsesGetVM.ToViewModel(model);
        }

        public QuestionResponsesGetVM Update(int id, int tenantId, QuestionResponsesUpdateVM request)
        {
            var response = _context.QuestionResponses
                .FirstOrDefault(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

            if (response == null) return null;

            response.ResponseText = request.ResponseText;
            response.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return QuestionResponsesGetVM.ToViewModel(response);
        }

        public bool Delete(int id, int tenantId)
        {
            var response = _context.QuestionResponses
                .FirstOrDefault(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

            if (response == null) return false;

            response.IsDeleted = true;
            response.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return true;
        }
    }
}
