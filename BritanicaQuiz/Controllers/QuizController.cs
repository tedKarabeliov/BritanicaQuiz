namespace BritanicaQuiz.Controllers
{
    using BritanicaQuiz.Appcode;
    using BritanicaQuiz.Appcode.Common;
    using BritanicaQuiz.Data;
    using BritanicaQuiz.Data.Services;
    using System;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class QuizController : Controller
    {
        private IQuizManager quizManager;
        private IQuestionService questionService;
        private IQuizEnrolmentService quizEnrolmentService;
        private IQuizService quizService;
        private ICompletedQuizzesHistoryService completedQuizzesHistoryService;

        public QuizController(IQuizManager quizManager, IQuestionService questionService,
            IQuizEnrolmentService quizEnrolmentService, IQuizService quizService,
            ICompletedQuizzesHistoryService completedQuizzesHistoryService)
        {
            this.quizManager = quizManager;
            this.questionService = questionService;
            this.quizEnrolmentService = quizEnrolmentService;
            this.quizService = quizService;
            this.completedQuizzesHistoryService = completedQuizzesHistoryService;
        }

        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FinalizeQuiz()
        {
            var user = User.Identity.GetUserId();

            var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(user);
            this.quizManager.CalculatePoints(enrolment.Id);

            var quizPoints = enrolment.TotalPoints;
            var totalPoints = this.quizService.GetQuizById(enrolment.QuizId).MaximumPoints;

            var mathOperations = new MathOperations();

            var percentageSuccess = mathOperations.CalculatePercentage(quizPoints, totalPoints);

            var nextQuizId = this.quizManager.GetNextQuiz(enrolment.Id, percentageSuccess);

            bool quizPreviouslyDone = false;
            if (nextQuizId != null)
            {
                quizPreviouslyDone = this.completedQuizzesHistoryService.FindQuizHistory(enrolment.Id, nextQuizId.Value) != null;
            }

            if (quizPreviouslyDone)
            {
                nextQuizId = null;
            }

            ViewData["quizPoints"] = quizPoints;
            ViewData["percentageSuccess"] = percentageSuccess;
            ViewData["nextQuizId"] = nextQuizId;

            return View();
        }

        public ActionResult ProceedToQuiz(int quizId)
        {
            var user = User.Identity.GetUserId();

            var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(user);

            var quizPreviouslyDone = this.completedQuizzesHistoryService.FindQuizHistory(enrolment.Id, quizId) != null;

            this.completedQuizzesHistoryService.AddQuizToHistory(enrolment.Id);
            if (quizPreviouslyDone)
            {
                return RedirectToAction("CompleteQuiz", "Quiz");
            }
            else
            {
                this.quizEnrolmentService.ResetPoints(enrolment.Id);
                this.quizEnrolmentService.ChangeQuizId(enrolment.Id, quizId);
            }

            Session["questionIndex"] = null;
            return RedirectToAction("Index", "Question");
        }

        public ActionResult CompleteQuiz()
        {
            var user = User.Identity.GetUserId();

            var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(user);

            Session["questionIndex"] = null;
            this.completedQuizzesHistoryService.AddQuizToHistory(enrolment.Id);
            this.quizEnrolmentService.CompleteEnrolment(enrolment.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}