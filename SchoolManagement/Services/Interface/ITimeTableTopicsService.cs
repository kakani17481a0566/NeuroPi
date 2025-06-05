using SchoolManagement.ViewModel.TimeTableTopics;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITimeTableTopicsService
    {
        TimeTableTopicResponseVM Create(TimeTableTopicRequestVM request);
        List<TimeTableTopicResponseVM> GetAll();
        List<TimeTableTopicResponseVM> GetAllByTenantId(int tenantId);
        TimeTableTopicResponseVM GetById(int id);
        TimeTableTopicResponseVM GetByIdAndTenantId(int id, int tenantId);
        TimeTableTopicResponseVM UpdateByIdAndTenantId(int id, int tenantId, TimeTableTopicUpdateVM request);
        TimeTableTopicResponseVM DeleteByIdAndTenantId(int id, int tenantId);
    }
}
