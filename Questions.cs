
namespace QuizProgram
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string text, List<string> options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public override string ToString()
        {
            return $"{Text} (Correct: {Options[CorrectAnswerIndex]})";
        }
    }
}