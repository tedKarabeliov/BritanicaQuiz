namespace BritanicaQuiz.Model.Questions
{
    using BritanicaQuiz.Model;

    public class OpenTextQuestion : Question
    {
        public override string HtmlPartial
        {
            get { return "~/Views/Shared/QuestionPartials/_OpenTextQuestion.cshtml"; }
        }
    }
}
