using SchoolManagement.ViewModel.Topic;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITopicService
    {

        List<TopicResponseVM> GetAll(); // Get all topics (no tenant filter)
        TopicResponseVM GetById(int id); // Get topic by ID (no tenant filter)

        List<TopicResponseVM> GetAll(int tenantId);
        TopicResponseVM GetById(int id, int tenantId);
        TopicResponseVM Create(TopicRequestVM request);
        TopicResponseVM Update(int id, int tenantId, TopicUpdateVM request);
        bool Delete(int id, int tenantId);
    }
}
