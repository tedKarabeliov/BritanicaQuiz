namespace BritanicaQuiz.Mailing
{
    using System.Collections.Generic;
    using System.Linq;
    using BritanicaQuiz.Data;
    using BritanicaQuiz.Data.Services;
    using BritanicaQuiz.Model;

    public class MailSender
    {
        private const string MandrillKey = "BritanicaMandrillKey";
        private const string FromMail = "no-reply@britanica-edu.org";
        private const string FromName = "BRITANICA";
        private const int SendMailHasCertificateTemplateId = 2257;

        private MandrillMailSender mandrill;
        private IMailTemplateService mailTemplateService;

        public MailSender(IMailTemplateService mailTemplateService)
        {
            this.mailTemplateService = mailTemplateService;
            this.mandrill = new MandrillMailSender(MandrillKey);
        }

        // This is sent when the person has earned a certificate in the past 12 months
        public void SendMailHasCertificate(string email, string firstName, string familyName)
        {
            var mailData = this.mailTemplateService.GetMailTemplate(SendMailHasCertificateTemplateId);

            var mailTemplateText = mailData.MailTemplateText;

            var templateVariables = new Dictionary<string, string>();

            templateVariables.Add("##FIRST_NAME##", firstName);
            templateVariables.Add("##FAMILY_NAME##", familyName);

            mailTemplateText =new MailTemplateVariableResolver()
                .ResolveTemplate(mailTemplateText, templateVariables);

            var taskResult = mandrill.SendMessage(FromMail,
                new List<string>() { email, "sent@britanica-edu.org" }, FromName, mailData.MailSubject, mailTemplateText);
        }
    }
}
