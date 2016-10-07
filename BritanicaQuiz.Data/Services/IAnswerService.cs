using BritanicaQuiz.Model;
using System.Collections.Generic;
namespace BritanicaQuiz.Data.Services
{
    public interface IAnswerService
    {
        Answer GetById(int answerId);

        IList<Answer> GetAllAnswers(int? questionId = null);

        Answer GetEmptyAnswer(int questionId);

        string EmptyAnswerText { get; }
    }
}
