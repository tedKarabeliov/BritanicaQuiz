namespace BritanicaQuiz.Data.Services
{
    using BritanicaQuiz.Model;
    using System.Collections.Generic;

    public interface IQuestionSetService
    {
        IList<QuestionSet> GetAllQuestionSets(int? quizId = null);
    }
}
