namespace BritanicaQuiz.Data
{
    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;
    using System;
    using System.Collections.Generic;

    public class BritanicaQuizData : IBritanicaQuizData
    {
        private BritanicaQuizDbContext context;
        private IDictionary<Type, object> repositories;

        public BritanicaQuizData(BritanicaQuizDbContext dbContext)
        {
            this.context = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public GenericRepository<User> Users { get { return this.GetRepository<User>(); } }

        public GenericRepository<Quiz> Quizzes { get { return this.GetRepository<Quiz>(); } }

        public GenericRepository<Question> Questions { get { return this.GetRepository<Question>(); } }

        public GenericRepository<QuestionSet> QuestionSets { get { return this.GetRepository<QuestionSet>(); } }

        public GenericRepository<Answer> Answers { get { return this.GetRepository<Answer>(); } }

        public GenericRepository<QuizResult> QuizResults { get { return this.GetRepository<QuizResult>(); } }

        public GenericRepository<QuizEnrolment> QuizEnrolments { get { return this.GetRepository<QuizEnrolment>(); } }

        public GenericRepository<CompletedQuizzesHistory> CompletedQuizzesHistory { get { return this.GetRepository<CompletedQuizzesHistory>(); } }

        public GenericRepository<Department> Departments { get { return this.GetRepository<Department>(); } }

        public GenericRepository<City> Cities { get { return this.GetRepository<City>(); } }

        public GenericRepository<MailTemplate> MailTemplates { get { return this.GetRepository<MailTemplate>(); } }

        public TrainingGroupsRepository TrainingGroups { get { return this.TrainingGroups; } }

        private GenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (GenericRepository<T>)this.repositories[typeOfModel];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
