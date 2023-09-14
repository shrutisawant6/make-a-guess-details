namespace Quiz.Models
{
    public class DisplayQuestionAnswer
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public List<char> Answer { get; set; }
    }
}
