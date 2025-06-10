using SchoolManagement.ViewModel.TimeTableTopics;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITimeTableTopicsService
    {
        List<TimeTableTopicResponseVM> GetAll();
        List<TimeTableTopicResponseVM> GetAllByTenantId(int tenantId);
        TimeTableTopicResponseVM GetById(int id);
        TimeTableTopicResponseVM GetByIdAndTenantId(int id, int tenantId);
        TimeTableTopicResponseVM Create(TimeTableTopicRequestVM request);
        TimeTableTopicResponseVM UpdateByIdAndTenantId(int id, int tenantId, TimeTableTopicUpdateVM request);
        bool Delete(int id, int tenantId);
    }
}
