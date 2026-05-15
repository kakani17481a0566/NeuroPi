using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionResponses;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionResponsesService : IQuestionResponsesService
    {
        private readonly SchoolManagementDb _context;

        public QuestionResponsesService(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<MVwEmployeeQuestionAnswers> GetEmployeeQuestionAnswers(int employeeId, int tenantId)
        {
            return _context.VwEmployeeQuestionAnswers
                .Where(x => x.EmployeeId == employeeId && x.TenantId == tenantId)
                .ToList();
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

        public List<QuestionResponsesListVM> GetByCandidate(int candidateId, int tenantId)
        {
            var data = _context.QuestionResponses
                .Where(r => r.CandidateId == candidateId && r.TenantId == tenantId && !r.IsDeleted)
                .ToList();

            return QuestionResponsesListVM.ToViewModelList(data);
        }

        public QuestionResponsesGetVM Create(QuestionResponsesCreateVM request)
        {
            var existing = _context.QuestionResponses
                .FirstOrDefault(r => r.TenantId == request.TenantId
                                  && r.PaperId == request.PaperId
                                  && r.CandidateId == request.CandidateId
                                  && r.QuestionId == request.QuestionId
                                  && !r.IsDeleted);
            if (existing != null)
            {
                return QuestionResponsesGetVM.ToViewModel(existing);
            }

            var model = QuestionResponsesCreateVM.ToModel(request);
            model.CreatedOn = DateTime.UtcNow;
            _context.QuestionResponses.Add(model);
            _context.SaveChanges();

            return QuestionResponsesGetVM.ToViewModel(model);
        }

        public List<QuestionResponsesGetVM> CreateBatch(QuestionResponsesBatchRequestVM request)
        {
            var results = new List<QuestionResponsesGetVM>();
            foreach (var item in request.Responses)
            {
                var createVm = new QuestionResponsesCreateVM
                {
                    TenantId = request.TenantId,
                    PaperId = request.PaperId,
                    CandidateId = request.CandidateId,
                    QuestionId = item.QuestionId,
                    ResponseText = item.ResponseText,
                    CreatedBy = request.CreatedBy
                };
                results.Add(Create(createVm));
            }
            return results;
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
