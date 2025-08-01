﻿using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.Topic;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITopicService
    {
        // Existing
        List<TopicResponseVM> GetAll();
        TopicResponseVM GetById(int id);

        List<TopicResponseVM> GetAll(int tenantId);
        TopicResponseVM GetById(int id, int tenantId);
        TopicResponseVM Create(TopicRequestVM request);
        TopicResponseVM Update(int id, int tenantId, TopicUpdateVM request);
        bool Delete(int id, int tenantId);
        TopicFullResponseVM GetResolvedTopics(int tenantId);
        TimeTableDropDown GetTimeTableDropDown(int tenantId);


    }
}
