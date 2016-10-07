namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;

    public class QuestionService : IQuestionService
    {
        private GenericRepository<Question> questionRepository;

        public QuestionService(GenericRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public IList<Question> GetAllQuestions(int? questionSetId = null)
        {
            var questions = this.questionRepository.All();

            if (questionSetId.HasValue)
            {
                questions = questions.Where(q => q.QuestionSetId== questionSetId);
            }

            return questions.ToList();
        }
    }
}
