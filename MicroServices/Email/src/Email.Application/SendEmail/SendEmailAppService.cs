using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;

namespace Email.SendEmail
{
    public class SendEmailAppService : ApplicationService, ISendEmailAppService
    {

        private readonly IBackgroundJobManager _backgroundJobManager;

        public SendEmailAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;

        }


        public async Task Send(EmailSendingArgs email)
        {
            await _backgroundJobManager.EnqueueAsync(email);
        }

        
    }
}
