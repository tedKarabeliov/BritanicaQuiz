using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BritanicaQuiz.Appcode.CalculatePoints
{
    public class ReadingQuestionCalculatePointsStrategy : ICalculateIsCorrectStrategy
    {
        public bool CalculateIsCorrectQuestion(Model.Answer answer, Model.QuizResult quizResult)
        {
            if (answer.IsCorrect && quizResult != null)
            {
                return true;
            }

            return false;
        }
    }
}