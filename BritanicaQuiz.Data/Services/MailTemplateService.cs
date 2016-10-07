namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BritanicaQuiz.Model;
    using BritanicaQuiz.Data.Repositories;

    public class MailTemplateService : IMailTemplateService
    {
        private GenericRepository<MailTemplate> mailTemplateRepository;

        public MailTemplateService(GenericRepository<MailTemplate> mailTemplateRepository)
        {
            this.mailTemplateRepository = mailTemplateRepository;
        }

        public IList<MailTemplate> GetAllMailTemplates()
        {
            return this.mailTemplateRepository.All().ToList();
        }

        public MailTemplate GetMailTemplate(int id)
        {
            return this.GetAllMailTemplates().FirstOrDefault(mt => mt.Id == id);
        }
    }
}
