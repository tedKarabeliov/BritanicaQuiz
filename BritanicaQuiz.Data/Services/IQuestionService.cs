namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;

    using BritanicaQuiz.Model;

    public interface IQuestionService
    {
        IList<Question> GetAllQuestions(int? questionSetId = null);
    }
}
