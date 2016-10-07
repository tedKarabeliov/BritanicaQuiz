namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using BritanicaQuiz.Model;
    using BritanicaQuiz.Data.Repositories;

    public class QuizService : IQuizService
    {
        private GenericRepository<Quiz> quizRepository;

        public QuizService(GenericRepository<Quiz> quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public IList<Quiz> GetAll()
        {
            return this.quizRepository.All().OrderBy(q => q.Name).ToList();
        }

        public Quiz GetQuizById(int quizId)
        {
            return this.quizRepository.All().FirstOrDefault(q => q.Id == quizId);
        }

        public Quiz GetQuizByName(string name)
        {
            return this.quizRepository.All().FirstOrDefault(q => q.Name == name);
        }

        public int? GetNextQuizId(int quizId)
        {
            var quiz = this.GetQuizById(quizId);

            return quiz.NextQuizId;
        }

        public int? GetPreviousQuizId(int quizId)
        {
            var quiz = this.GetQuizById(quizId);

            var previousQuiz = this.quizRepository.All().FirstOrDefault(q => q.NextQuizId == quizId);

            if (previousQuiz == null)
            {
                return null;
            }

            return previousQuiz.Id;
        }
    }
}
