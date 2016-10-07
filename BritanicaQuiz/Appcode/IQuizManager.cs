namespace BritanicaQuiz.Appcode
{
    public interface IQuizManager
    {
        void CalculatePoints(int enrolmentId);

        int? GetNextQuiz(int quizEnrolmentId, int totalPoints);
    }
}
