﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Email.SendEmail
{
    public interface ISendEmailAppService : IApplicationService
    {
         Task Send(EmailSendingArgs email);
    }
}
