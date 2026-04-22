using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel;
using SchoolManagement.ViewModel.Questions;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService _questionService)
        {
            questionService=_questionService;            
        }

        [HttpGet("/questions/{tenantId}")]
        public ResponseResult<List<QuestionsVM>> GetQuestions(int tenantId)
        {
            var response=questionService.getAllQuestions(tenantId);
            if (response == null)
                return new ResponseResult<List<QuestionsVM>>(HttpStatusCode.NotFound, null, "Questions not found");
            return new ResponseResult<List<QuestionsVM>>(HttpStatusCode.OK, response, "Questions fetched successfully");
        }
    }
}
