namespace BritanicaQuiz.Model.Questions
{
    using BritanicaQuiz.Model;

    public class InputAnswerQuestion : Question
    {
        public override string HtmlPartial
        {
            get { return "~/Views/Shared/QuestionPartials/_InputAnswerQuestion.cshtml"; }
        }
    }
}