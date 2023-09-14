using Quiz.Models;
using Quiz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {

        private readonly ILogger<QuizController> _logger;

        private readonly IDataService _dataService;

        private static QuestionAnswerResponse? data;

        public QuizController(ILogger<QuizController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;

            data ??= _dataService.GetData();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetResponse")]
        public DisplayQuestionAnswer GetResponse(DisplayQuestionAnswerRequest request)
        {
           return _dataService.GetSeletectedData(request, data);
        }
    }
}