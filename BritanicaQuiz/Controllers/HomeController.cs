using BritanicaQuiz.Data;
using System.Web.Mvc;
using System.Linq;
using BritanicaQuiz.Model;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Text;
using BritanicaQuiz.Appcode;
using BritanicaQuiz.Data.Services;
using BritanicaQuiz.ViewModels;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BritanicaQuiz.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ITrainingGroupService trainingGroupService;
        private IQuizEnrolmentService quizEnrolmentService;
        private IQuizService quizService;

        public HomeController(ITrainingGroupService trainingGroupService, IQuizEnrolmentService quizEnrolmentService
            , IQuizService quizService)
        {
            this.trainingGroupService = trainingGroupService;
            this.quizEnrolmentService = quizEnrolmentService;
            this.quizService = quizService;
        }

        [HttpPost]
        public ActionResult SubmitAction(string info)
        {
            var i = Request["info"];
            byte[] data = Convert.FromBase64String(i);
            string decodedString = Encoding.UTF8.GetString(data);
            string s = decodedString;
            string[] values = s.Split('&');

            string name = values[0];
            string familyname = values[1];
            string email = values[2];
            Session["name"] = name;
            Session["familyname"] = familyname;
            Session["email"] = email;

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            return View();
        }

        public ActionResult TrainingGroups_Read([DataSourceRequest]DataSourceRequest request)
        {
            var enrolment = this.quizEnrolmentService.GetLastEnrolmentForUser(User.Identity.GetUserId());

            Quiz quiz = null;

            if (enrolment != null)
            {
                quiz = this.quizService.GetQuizById(enrolment.Id);
            }

            var trainingGroups = this.trainingGroupService.GetAllTrainingGroups(quiz != null ? quiz.Name : string.Empty)
                .Select(tg => new TrainingGroupViewModel()
                {
                    TrainingProduct = tg.TrainingProduct,
                    StartingDate = tg.StartingDate
                });

            return Json(trainingGroups.ToDataSourceResult(request));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}