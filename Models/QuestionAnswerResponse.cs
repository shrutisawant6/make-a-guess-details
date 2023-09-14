namespace Quiz.Models
{
    public class QuestionAnswerResponse
    {
        public List<QuestionAnswer> Data { get; set; }

        public QuestionAnswerResponse()
        {
            Data = new List<QuestionAnswer>();
        }
    }
}
