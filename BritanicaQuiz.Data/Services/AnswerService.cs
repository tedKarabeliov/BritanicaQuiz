namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;

    public class AnswerService : IAnswerService
    {
        private const string EmptyAnswerTextConstant = "EmptyAnswer";

        private GenericRepository<Answer> answerRepository;

        public string EmptyAnswerText { get { return EmptyAnswerTextConstant; } }

        public AnswerService(GenericRepository<Answer> answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        public Answer GetById(int answerId)
        {
            return this.answerRepository.All().FirstOrDefault(a => a.Id == answerId);
        }

        public IList<Answer> GetAllAnswers(int? questionId = null)
        {
            var answers = this.answerRepository.All().Where(a => a.Text != EmptyAnswerText);

            if (questionId.HasValue)
            {
                answers = answers.Where(q => q.QuestionId == questionId);
            }

            return answers.ToList();
        }

        public Answer GetEmptyAnswer(int questionId)
        {
            return this.answerRepository.All().FirstOrDefault(
                a => a.QuestionId == questionId && a.Text == EmptyAnswerText);
        }
    }
}
