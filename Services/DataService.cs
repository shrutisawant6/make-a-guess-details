using Quiz.Models;
using System.Text.Json;

namespace Quiz.Services
{
    public interface IDataService
    {
        QuestionAnswerResponse? GetData();

        DisplayQuestionAnswer GetSeletectedData(DisplayQuestionAnswerRequest request, QuestionAnswerResponse? result);
    }

    public class DataService : IDataService
    {
        private static readonly string GameDoneQuestion = "What happens when the game comes to an end ?";
        private static readonly string GameDoneAnswer = "Game Over";
        private static readonly string ErrorQuestion = "Oops, something went wrong!";
        private static readonly string ErrorAnswer = "Unanswered";

        public static readonly int GameOverCode = 99990;
        public static readonly int ErrorCode = 99999;

        public DataService()
        {
        }

        public QuestionAnswerResponse? GetData()
        {
            using StreamReader streamReader = new("data.json");
            var json = streamReader.ReadToEnd();
            var response = JsonSerializer.Deserialize<QuestionAnswerResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public DisplayQuestionAnswer GetSeletectedData(DisplayQuestionAnswerRequest request, QuestionAnswerResponse? result)
        {
            try
            {
                if (result?.Data != null)
                {
                    var orderedList = result.Data.OrderBy(val => val.Id);
                    int indexValue = request.Id == ErrorCode ? 0 : (request.Id + 1);
                    var lastIndex = result.Data.OrderByDescending(val => val.Id).FirstOrDefault()?.Id;

                    if (request.Id == lastIndex)
                    {
                        return new DisplayQuestionAnswer
                        {
                            Id = GameOverCode,
                            Question = GameDoneQuestion,
                            Answer = GameDoneAnswer.ToUpper().ToCharArray().ToList()
                        };
                    }

                    var selectedResponse = result.Data.Where(val => val.Id == indexValue).FirstOrDefault();
                    if (selectedResponse != null)
                        return new DisplayQuestionAnswer
                        {
                            Id = selectedResponse.Id,
                            Question = selectedResponse.Question,
                            Answer = selectedResponse.Answer.ToUpper().ToCharArray().ToList()
                        };

                }
            }
            catch (Exception)
            {
            }

            return new DisplayQuestionAnswer
            {
                Id = ErrorCode,
                Question = ErrorQuestion,
                Answer = ErrorAnswer.ToUpper().ToCharArray().ToList()
            };
        }

    }
}
