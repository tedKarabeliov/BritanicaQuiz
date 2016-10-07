namespace BritanicaQuiz.Data.Services
{
    using System.Linq;

    using BritanicaQuiz.Model;

    public class CompletedQuizzesHistoryService : ICompletedQuizzesHistoryService
    {
        IBritanicaQuizData db;
        IQuizEnrolmentService quizEnrolmentService;

        public CompletedQuizzesHistoryService(IBritanicaQuizData db, IQuizEnrolmentService quizEnrolmentService)
        {
            this.db = db;
            this.quizEnrolmentService = quizEnrolmentService;
        }

        public CompletedQuizzesHistory FindQuizHistory(int enrolmentId, int quizId)
        {
            if (quizId == null)
            {
                return null;
            }

            return this.db.CompletedQuizzesHistory.All()
                .FirstOrDefault(cqh => cqh.QuizEnrolmentId == enrolmentId && cqh.QuizId == quizId);

        }

        public void AddQuizToHistory(int enrolmentId)
        {
            var enrolment = this.quizEnrolmentService.GetEnrolment(enrolmentId);

            var quizHistory = new CompletedQuizzesHistory()
            {
                QuizEnrolmentId = enrolment.Id,
                QuizId = enrolment.QuizId,
                Points = enrolment.TotalPoints
            };

            this.db.CompletedQuizzesHistory.Add(quizHistory);
        }
    }
}
