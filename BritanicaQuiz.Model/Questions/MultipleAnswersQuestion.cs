namespace BritanicaQuiz.Model.Questions
{
    using BritanicaQuiz.Model;

    public class MultipleAnswersQuestion : Question
    {
        public override string HtmlPartial
        {
            get { return "~/Views/Shared/QuestionPartials/_MultipleAnswersQuestion.cshtml"; }
        }
    }
}
