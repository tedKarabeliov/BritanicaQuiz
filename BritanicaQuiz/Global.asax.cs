namespace BritanicaQuiz
{
    using BritanicaQuiz.Appcode;
    using BritanicaQuiz.Data;
    using BritanicaQuiz.Data.BritanicaOnlineExamRegistrationService;
    using BritanicaQuiz.Data.Services;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<BritanicaQuizDbContext>().ToSelf().InSingletonScope();
            kernel.Bind<IBritanicaQuizData>().To<BritanicaQuizData>().InSingletonScope();
            kernel.Bind<IQuizService>().To<QuizService>().InSingletonScope();
            kernel.Bind<IQuestionService>().To<QuestionService>().InSingletonScope();
            kernel.Bind<IQuizEnrolmentService>().To<QuizEnrolmentService>().InSingletonScope();
            kernel.Bind<IQuizResultService>().To<QuizResultService>().InSingletonScope();
            kernel.Bind<IAnswerService>().To<AnswerService>().InSingletonScope();
            kernel.Bind<IQuestionSetService>().To<QuestionSetService>().InSingletonScope();
            kernel.Bind<ICompletedQuizzesHistoryService>().To<CompletedQuizzesHistoryService>();
            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<ICityService>().To<CityService>();
            kernel.Bind<IMailTemplateService>().To<MailTemplateService>();
            kernel.Bind<ITrainingGroupService>().To<TrainingGroupService>();
            kernel.Bind<IQuestionManager>().To<QuestionManager>().InSingletonScope();
            kernel.Bind<IQuestionSetCollection>().ToProvider(new QuestionCollectionSetProvider());
            kernel.Bind<IQuizManager>().To<QuizManager>();
            kernel.Bind<ICRMWebServiceManager>().To<CRMWebServiceManager>();
            kernel.Bind<BritanicaOnlineExamRegistrationSoapClient>().ToSelf();

            return kernel;
        }
    }
}
