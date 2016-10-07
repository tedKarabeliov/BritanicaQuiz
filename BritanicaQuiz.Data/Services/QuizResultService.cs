namespace BritanicaQuiz.Data.Services
{
    using System;
    using System.Linq;

    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;

    public class QuizResultService : IQuizResultService
    {
        private GenericRepository<QuizResult> quizResultRepository;
        private GenericRepository<Answer> answerRepository;

        public QuizResultService(GenericRepository<QuizResult> quizResultRepository, GenericRepository<Answer> answerRepository)
        {
            this.quizResultRepository = quizResultRepository;
            this.answerRepository = answerRepository;
        }

        public void AddQuizResult(int answerId, int enrolmentId, string text, TimeSpan timeSpent)
        {
            var quizResult = new QuizResult()
             {
                 AnswerId = answerId,
                 QuizEnrolmentId = enrolmentId,
                 Text = text,
                 TimeSpent = timeSpent
             };

            this.quizResultRepository.Add(quizResult);
            this.quizResultRepository.SaveChanges();
        }

        public bool QuizResultExists(int questionId, int enrolmentId)
        {
            var answers = answerRepository.All().Where(a => a.QuestionId == questionId);

            return this.quizResultRepository.All().Any(qr => answers.Contains(qr.Answer) && qr.QuizEnrolmentId == enrolmentId);
        }

        public void DeleteQuizResultsForQuestion(int questionId, int enrolmentId)
        {
            var answers = answerRepository.All().Where(a => a.QuestionId == questionId);

            var quizResults = this.quizResultRepository.All().Where(qr => answers.Contains(qr.Answer) && qr.QuizEnrolmentId == enrolmentId);

            foreach (var quizResult in quizResults)
            {
                this.quizResultRepository.Delete(quizResult);
            }

            this.quizResultRepository.SaveChanges();
        }
    }
}
