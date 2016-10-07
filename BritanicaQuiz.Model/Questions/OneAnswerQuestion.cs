namespace BritanicaQuiz.Model.Questions
{
    using BritanicaQuiz.Model;

    public class OneAnswerQuestion : Question
    {
        public override string HtmlPartial
        {
            get { return "~/Views/Shared/QuestionPartials/_OneAnswerQuestion.cshtml"; }
        }
    }
}
