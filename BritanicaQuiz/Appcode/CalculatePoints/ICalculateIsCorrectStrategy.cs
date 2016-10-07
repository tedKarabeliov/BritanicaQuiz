namespace BritanicaQuiz.Appcode.CalculatePoints
{
    using BritanicaQuiz.Model;

    public interface ICalculateIsCorrectStrategy
    {
        bool CalculateIsCorrectQuestion(Answer answer, QuizResult quizResult);
    }
}
