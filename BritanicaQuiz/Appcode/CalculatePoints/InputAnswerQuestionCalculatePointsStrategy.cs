namespace BritanicaQuiz.Appcode.CalculatePoints
{
    using BritanicaQuiz.Model;

    public class InputAnswerQuestionCalculatePointsStrategy : ICalculateIsCorrectStrategy
    {
        public bool CalculateIsCorrectQuestion(Answer answer, QuizResult quizResult)
        {
            if (quizResult == null)
            {
                return false;
            }

            var initialAnswerText = answer.CaseSensitive ? answer.Text : answer.Text.ToLower();

            var quizResultText = answer.CaseSensitive ? quizResult.Text : quizResult.Text.ToLower();

            var answerTextsSplitted = initialAnswerText.Split(new char[] { '|' });

            foreach (var answerText in answerTextsSplitted)
            {
                if (answerText == quizResultText)
                {
                    return true;
                }
            }

            return false;
        }
    }
}