using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.MailKit;
using Volo.Abp.Settings;

namespace Email.SendEmail
{
    public class EmailSendingJob : AsyncBackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly IMailKitSmtpEmailSender _mailKitSmtpEmailSender;

        public EmailSendingJob(IEmailSender emailSender, IMailKitSmtpEmailSender mailKitSmtpEmailSender)
        {
            _emailSender = mailKitSmtpEmailSender;
            _mailKitSmtpEmailSender = mailKitSmtpEmailSender;
        }

        public override async Task ExecuteAsync(EmailSendingArgs args)
        {
            Logger.LogInformation("IMailKitSmtpEmailSender executing SendEmailJob ,to: {0}", args.EmailAddress);
            await _emailSender.SendAsync(
                args.EmailAddress,
                args.Subject,
                args.Body
            );

        }
    }
}
