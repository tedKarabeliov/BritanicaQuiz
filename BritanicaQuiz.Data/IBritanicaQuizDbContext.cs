using BritanicaQuiz.Model;
using System.Data.Entity;
namespace BritanicaQuiz.Data
{
    public interface IBritanicaQuizDbContext
    {
        IDbSet<Quiz> Quizzes { get; set; }

        IDbSet<Question> Questions { get; set; }

        IDbSet<QuestionSet> QuestionSets { get; set; }

        IDbSet<Answer> Answers { get; set; }

        IDbSet<QuizResult> QuizResults { get; set; }

        IDbSet<QuizEnrolment> QuizEnrolments { get; set; }

        IDbSet<CompletedQuizzesHistory> CompletedQuizzesHistory { get; set; }

        IDbSet<Department> Departments { get; set; }

        IDbSet<MailTemplate> MailTemplates { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<T> Set<T>() where T : class;
    }
}
