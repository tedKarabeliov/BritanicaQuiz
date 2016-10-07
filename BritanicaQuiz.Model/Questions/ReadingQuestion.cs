namespace BritanicaQuiz.Model.Questions
{
    using System.Collections.Generic;

    public class ReadingQuestion : Question
    {
        public override string HtmlPartial
        {
            get { return "~/Views/Shared/QuestionPartials/_ReadingQuestion.cshtml"; }
        }
    }
}
