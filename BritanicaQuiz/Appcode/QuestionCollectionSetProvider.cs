namespace BritanicaQuiz.Appcode
{
    using Ninject;
    using Ninject.Activation;
    using System.Web;

    public class QuestionCollectionSetProvider : Provider<QuestionSetCollection>
    {
        private const string QuestionCollection = "questionCollection";

        protected override QuestionSetCollection CreateInstance(IContext context)
        {
            var session = HttpContext.Current.Session;

            if (session[QuestionCollection] == null)
            {
                session[QuestionCollection] = new QuestionSetCollection();
            }

            return (session[QuestionCollection] as QuestionSetCollection);
        }
    }
}