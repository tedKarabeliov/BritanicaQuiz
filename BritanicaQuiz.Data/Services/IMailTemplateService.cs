namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;

    using BritanicaQuiz.Model;

    public interface IMailTemplateService
    {
        IList<MailTemplate> GetAllMailTemplates();

        MailTemplate GetMailTemplate(int id);
    }
}
