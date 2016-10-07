namespace BritanicaQuiz.Mailing
{
    using Mandrill;
    using Mandrill.Models;
    using Mandrill.Requests.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class MandrillMailSender
    {
        private MandrillApi mandrillApp;

        public MandrillMailSender(string mandrillAppKey)
        {
            this.mandrillApp = new MandrillApi(mandrillAppKey, false);
        }

        public async Task<ICollection<EmailResult>> SendMessage(string from, ICollection<string> receivers, string fromName, string subject, string content)
        {
            var message = new EmailMessage();

            message.FromEmail = from;
            message.FromName = fromName;
            message.Subject = subject;

            message.To = receivers.Select(r => new EmailAddress(r));

            message.Html = content;

            var request = new SendMessageRequest(message);
            var result = await this.mandrillApp.SendMessage(request);

            return result;
        }
    }
}
