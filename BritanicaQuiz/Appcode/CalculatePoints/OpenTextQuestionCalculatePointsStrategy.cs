namespace BritanicaQuiz.Appcode.CalculatePoints
{
    using BritanicaQuiz.Model;

    public class OpenTextQuestionCalculatePointsStrategy : ICalculateIsCorrectStrategy
    {
        public bool CalculateIsCorrectQuestion(Answer answer, QuizResult quizResult)
        {
            return false;
        }
    }
}
