namespace BritanicaQuiz.Appcode.CalculatePoints
{
    using BritanicaQuiz.Model;

    public class MultipleAnswersQuestionCalculatePointsStrategy : ICalculateIsCorrectStrategy
    {
        public bool CalculateIsCorrectQuestion(Answer answer, QuizResult quizResult)
        {
            if (answer.IsCorrect && quizResult != null)
            {
                return true;
            }

            return false;
        }
    }
}
