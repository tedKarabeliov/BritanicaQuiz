namespace BritanicaQuiz.Controllers
{
    using BritanicaQuiz.Appcode;
    using BritanicaQuiz.Data;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using BritanicaQuiz.Model;
    using BritanicaQuiz.ViewModels;
    using BritanicaQuiz.Data.Services;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class QuestionController : Controller
    {
        private IBritanicaQuizData db;
        private IQuestionSetCollection questionSetCollection;
        private IQuestionManager questionManager;
        private IQuestionService questionService;
        private IAnswerService answerService;
        private IQuizResultService quizResultService;
        private IQuizService quizService;
        private IQuizEnrolmentService quizEnrolmentService;
        private IQuestionSetService questionSetService;

        public QuestionController(IBritanicaQuizData db, IQuestionManager questionManager, IQuestionSetCollection questionCollection,
            IQuizService quizService, IQuestionService questionService, IAnswerService answerService, IQuizResultService quizResultService, IQuizEnrolmentService quizEnrolmentService, IQuestionSetService questionSetService)
        {
            this.db = db;

            this.questionSetCollection = questionCollection;
            this.questionManager = questionManager;

            this.quizService = quizService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.quizResultService = quizResultService;
            this.quizEnrolmentService = quizEnrolmentService;
            this.questionSetService = questionSetService;
        }

        public ActionResult Index()
        {
            var user = this.User.Identity.GetUserId();

            var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(user);
            var quiz = this.quizService.GetQuizById(enrolment.QuizId);

            if (Session["questionIndex"] == null)
            {
                var quizQuestionsSets = this.questionSetService.GetAllQuestionSets(quiz.Id);
                var shuffledQuestionSets = this.questionManager.ShuffleQuestionSets(quizQuestionsSets, new Random());
                this.questionSetCollection.AssignQuestionSets(shuffledQuestionSets);

                Session.Add("questionIndex", this.questionSetCollection.Index);
                Session.Add("currentQuizEnrolmentId", enrolment.Id);
            }

            ViewData["QuizName"] = quiz.Name;

            return View();
        }

        public ActionResult AjaxContent(bool? next)
        {
            var user = User.Identity.GetUserId();

            var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(user);

            if ((this.questionSetCollection.Full && next == true))
            {
                return RedirectToAction("FinalizeQuiz", "Quiz");
            }

            QuestionSet questionSet;

            if (!(next.HasValue))
            {
                questionSet = this.questionSetCollection.GetCurrentQuestionSet();
            }
            else if (next.HasValue && next == true)
            {
                questionSet = this.questionSetCollection.GetNextQuestionSet();
            }
            else
            {
                questionSet = this.questionSetCollection.GetPreviousQuestionSet();
            }

            var questionSetToReturn = new QuestionSetViewModel()
            {
                Text = questionSet.Text,
                AnswerTime = questionSet.AnswerTime,
                Questions = questionSet.Questions.Select(q => new QuestionViewModel()
                {
                    Id = q.Id,
                    Text = q.Text,
                    HtmlPartial = q.HtmlPartial,
                    Answers = q.Answers.Where(a => a.Text != "EmptyAnswer").Select(a => new AnswerViewModel() {
                        Id = a.Id,
                        Text = a.Text,
                        QuestionId = q.Id,
                        QuizResults = a.QuizResults.Where(qr => qr.QuizEnrolmentId == enrolment.Id).ToList()
                    }).ToList(),
                }).ToList()
            };

            ViewData["questionSetIndex"] = this.questionSetCollection.Index;
            ViewData["questionSetsCount"] = this.questionSetCollection.Count;
            ViewData["questionIndex"] = 0;
            ViewData["isAnswered"] = this.questionManager.IsAnswered(enrolment.Id, questionSet);

            return View("AjaxContent", questionSetToReturn);
        }

        [HttpPost]
        public ActionResult NextQuestion(QuizResultInputViewModel result)
        {
            var user = User.Identity.GetUserId();
            var enrolment = quizEnrolmentService.GetActiveEnrolmentForUser(user);

            var questionSetToAnswer = this.questionSetCollection.GetCurrentQuestionSet();

            var timeleft = new TimeSpan(result.Hours, result.Minutes, result.Seconds);

            foreach (var question in questionSetToAnswer.Questions)
            {
                if (!(quizResultService.QuizResultExists(question.Id, enrolment.Id)))
                {
                    List<int> answerIds = null;
                    List<string> answerTexts = null;

                    if (result.Answer != null)
                    {
                        answerIds = this.questionManager.ExtractAnswerIdsForQuestion(question, result.Answer);
                    }

                    if (result.AnswerText != null)
                    {
                        answerTexts = this.questionManager.ExtractAnswerTextsForQuestion(question, result.AnswerText, result.TextAnswerIds);
                    }

                    this.questionManager.AnswerQuestion(question, enrolment.Id, answerIds, answerTexts, timeleft);
                }
            }

            return new HttpStatusCodeResult(200);
        }
    }
}