namespace BritanicaQuiz.Appcode
{
    using System.Collections.Generic;
    using System.Linq;

    using BritanicaQuiz.Model;
    using BritanicaQuiz.Appcode.Exceptions;
    using Ninject;
    using System.Web;
    using System;

    public class QuestionSetCollection : IQuestionSetCollection
    {
        private IList<QuestionSet> questionSets;
        private int index;

        public int Index
        {
            get { return this.index; }
            private set { this.index = value; }
        }

        public int Count
        {
            get { return this.questionSets.Count; }
        }

        public bool Full
        {
            get { return this.Index == (this.questionSets.Count - 1) ? true : false; }
        }

        public void AssignQuestionSets(IList<QuestionSet> questionSets)
        {
            if (questionSets.Count == 0)
            {
                throw new QuestionException("Empty collection given to question collection");
            }

            this.questionSets = questionSets;
            this.Reset();
        }

        public QuestionSet GetNextQuestionSet()
        {
            if (this.Full)
            {
                throw new QuestionException("Question index has passed beyond the end of the array");
            }

            this.Index++;

            var questionToReturn = this.questionSets[this.Index];

            return questionToReturn;
        }

        public QuestionSet GetCurrentQuestionSet()
        {
            if (this.Index < 0 || this.Index >= this.questionSets.Count)
            {
                throw new QuestionException("Question index outside bound of the question collection");
            }

            return this.questionSets[this.Index];
        }

        public QuestionSet GetPreviousQuestionSet()
        {
            if (this.Index <= 0)
            {
                throw new QuestionException("Question index has passed beyond the beginning of the array");
            }

            var questionSetToReturn = this.questionSets[--this.Index];

            return questionSetToReturn;
        }

        public void Reset()
        {
            this.Index = 0;
        }

        public static QuestionSetCollection GetInstance(IKernel kernelForInjection)
        {
            var instance = HttpContext.Current.Session["questionSetCollection"];

            if (instance == null)
            {
                HttpContext.Current.Session["questionSetCollection"] = kernelForInjection.Get<IQuestionSetCollection>();
                instance = HttpContext.Current.Session["questionSetCollection"];
                kernelForInjection.Inject(instance);
            }

            return (QuestionSetCollection)instance;
        }
    }
}