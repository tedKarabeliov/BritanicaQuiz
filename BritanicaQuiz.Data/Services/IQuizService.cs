using BritanicaQuiz.Model;
using System.Collections.Generic;
namespace BritanicaQuiz.Data.Services
{
    public interface IQuizService
    {
        Quiz GetQuizById(int quizId);

        Quiz GetQuizByName(string name);

        int? GetNextQuizId(int quizId);

        int? GetPreviousQuizId(int quizId);

        IList<Quiz> GetAll();
    }
}
