namespace BritanicaQuiz.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using BritanicaQuiz.Data;
    using BritanicaQuiz.Data.Services;
    using BritanicaQuiz.Mailing;
    using BritanicaQuiz.ViewModels;

    

    public class QuizEnrolmentController : Controller
    {
        private IQuizService quizService;
        private IQuizEnrolmentService quizEnrolmentService;
        private IDepartmentService departmentService;
        private ICityService cityService;
        private IMailTemplateService mailTemplateService;

        public QuizEnrolmentController(IQuizService quizService,
            IQuizEnrolmentService quizEnrolmentService, IDepartmentService departmentService,
            ICityService cityService, IMailTemplateService mailTemplateService)
        {
            this.quizService = quizService;
            this.quizEnrolmentService = quizEnrolmentService;
            this.departmentService = departmentService;
            this.cityService = cityService;
            this.mailTemplateService = mailTemplateService;
        }

        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();

            if (quizEnrolmentService.GetActiveEnrolmentForUser(user) != null)
            {
                return RedirectToAction("Index", "Question");
            }
            else
            {
                ViewData["quizzes"] = this.quizService.GetAll()
                    .Select(q => new QuizViewModel
                    {
                        Id = q.Id,
                        Name = q.Name,
                        ShortDescription = q.ShortDescription,
                        LongDescription = q.LongDescription
                    }).ToList();

                var dropdownStudyingCities = new List<string>() { "София", "Пловдив", "Бургас", "Варна" };

                ViewData["cities"] = this.cityService.GetAllCities().Where(c => dropdownStudyingCities.Contains(c.Name))
                    .OrderBy(c => dropdownStudyingCities.IndexOf(c.Name))
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();

                ViewData["departments"] = this.departmentService.GetAllDepartments()
                    .Select(c => new DepartmentViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();

                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateEnrolment(QuizEnrolmentInputViewModel quizEnrolmentInput)
        {
            if (!(ModelState.IsValid))
            {
                return View(quizEnrolmentInput);
            }

            this.quizEnrolmentService.CreateEnrolment(quizEnrolmentInput.CourseType, quizEnrolmentInput.PastEnglishStudyingDescription,
                quizEnrolmentInput.EnglishGoalsDescription, quizEnrolmentInput.DepartmentId,
                quizEnrolmentInput.CityId, quizEnrolmentInput.hasCertificate, quizEnrolmentInput.CertificateDescription,
                quizEnrolmentInput.CertificateGrade, quizEnrolmentInput.QuizId, User.Identity.GetUserId());

            if (quizEnrolmentInput.CertificateTime == "1")
            {
                var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(User.Identity.GetUserId());

                this.quizEnrolmentService.CompleteEnrolment(enrolment.Id);

                var userEmail = User.Identity.GetUserName();

                var user = HttpContext.GetOwinContext().GetUserManager<UserManager>().FindById(User.Identity.GetUserId());

                var mailSender = new MailSender(this.mailTemplateService);
                mailSender.SendMailHasCertificate(userEmail, user.FirstName, user.FamilyName);

                return RedirectToAction("Index", "Home");
            }

            
            //var enrolment = this.quizEnrolmentService.GetActiveEnrolmentForUser(user);

            return RedirectToAction("Index", "Question");
        }
    }
}