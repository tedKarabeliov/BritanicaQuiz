namespace BritanicaQuiz.Data.Services
{
    using System;

    public interface IQuizResultService
    {
        void AddQuizResult(int answerId, int enrolmentId, string text, TimeSpan timeSpent);

        bool QuizResultExists(int questionId, int enrolmentId);

        void DeleteQuizResultsForQuestion(int questionId, int enrolmentId);
    }
}
