namespace Quiz.Models
{
    public class QuestionAnswer
    {
        static int idCount = 0;

        private int _id;
        public int Id
        {
            get { return _id; }
        }

        public string Question { get; set; }

        public string Answer { get; set; }

        public QuestionAnswer()
        {
            _id = idCount;
            idCount++;
        }
    }
}
