namespace BritanicaQuiz.Data.Services
{
    using BritanicaQuiz.Model;

    public interface ICompletedQuizzesHistoryService
    {
        void AddQuizToHistory(int enrolmentId);

        CompletedQuizzesHistory FindQuizHistory(int enrolmentId, int quizId);
    }
}
