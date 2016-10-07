using BritanicaQuiz.Data.Repositories;
using BritanicaQuiz.Model;
namespace BritanicaQuiz.Data
{
    public interface IBritanicaQuizData
    {
        GenericRepository<User> Users { get; }

        GenericRepository<Quiz> Quizzes { get; }

        GenericRepository<Question> Questions { get; }

        GenericRepository<QuestionSet> QuestionSets { get; }

        GenericRepository<Answer> Answers { get; }

        GenericRepository<QuizResult> QuizResults { get; }

        GenericRepository<QuizEnrolment> QuizEnrolments { get; }

        GenericRepository<CompletedQuizzesHistory> CompletedQuizzesHistory { get; }

        GenericRepository<Department> Departments { get; }

        GenericRepository<City> Cities { get; }

        GenericRepository<MailTemplate> MailTemplates { get; }

        int SaveChanges();

        void Dispose();
    }
}
