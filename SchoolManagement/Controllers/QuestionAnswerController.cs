using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionAnswer;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IQuestionAnswerService questionAnswerService;
        public QuestionAnswerController(IQuestionAnswerService _questionService)
        {
            questionAnswerService= _questionService;
            
        }
        [HttpPost]
        public ResponseResult<string> AddAnswers([FromBody]QuestionAnswerVM Answers)
        {
            var result = questionAnswerService.AddAnswers(Answers);
            if (result != null)
                return new ResponseResult<string>(HttpStatusCode.Created, result, " Answered the questions Successfully");
            return new ResponseResult<string>(HttpStatusCode.NotAcceptable, result, "Not Answered ");
        }

    }
}
