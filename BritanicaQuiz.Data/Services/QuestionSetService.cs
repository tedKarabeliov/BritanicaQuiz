namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;

    public class QuestionSetService : IQuestionSetService
    {
        private GenericRepository<QuestionSet> questionSetRepository;

        public QuestionSetService(GenericRepository<QuestionSet> questionSetRepository)
        {
            this.questionSetRepository = questionSetRepository;
        }

        public IList<QuestionSet> GetAllQuestionSets(int? quizId = null)
        {
            var questionSets = this.questionSetRepository.All();

            if (quizId.HasValue)
            {
                questionSets = questionSets.Where(qs => qs.QuizId == quizId);
            }

            return questionSets.ToList();
        }
    }
}
