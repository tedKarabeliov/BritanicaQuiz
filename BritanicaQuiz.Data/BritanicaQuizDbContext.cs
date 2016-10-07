using BritanicaQuiz.Data.Configurations;
using BritanicaQuiz.Data.Migrations;
using BritanicaQuiz.Model;
using BritanicaQuiz.Model.Questions;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritanicaQuiz.Data
{
    public class BritanicaQuizDbContext : IdentityDbContext<User>
    {
        public BritanicaQuizDbContext()
            : base("BritanicaQuizConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer<BritanicaQuizDbContext>(
            //    new MigrateDatabaseToLatestVersion<BritanicaQuizDbContext, Configuration>());
        }

        public virtual IDbSet<Quiz> Quizzes { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<QuestionSet> QuestionSets { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        public virtual IDbSet<QuizResult> QuizResults { get; set; }

        public virtual IDbSet<QuizEnrolment> QuizEnrolments { get; set; }

        public virtual IDbSet<CompletedQuizzesHistory> CompletedQuizzesHistory { get; set; }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<MailTemplate> MailTemplates { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .Map<InputAnswerQuestion>(f => f.Requires("Type").HasValue(0))
                .Map<OneAnswerQuestion>(f => f.Requires("Type").HasValue(1))
                .Map<MultipleAnswersQuestion>(f => f.Requires("Type").HasValue(2))
                .Map<OpenTextQuestion>(f => f.Requires("Type").HasValue(3))
                .Map<ReadingQuestion>(f => f.Requires("Type").HasValue(4));

            modelBuilder.Entity<QuizEnrolment>()
                .HasRequired(qe => qe.Quiz)
                .WithMany(qe => qe.ChosenTeacherQuizEnrolments)
                .HasForeignKey(qe => qe.QuizId);

            modelBuilder.Configurations.Add(new DepartmentViewConfiguration ());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public static BritanicaQuizDbContext Create()
        {
            return new BritanicaQuizDbContext();
        }
    }
}
