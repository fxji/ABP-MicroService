using System;
using System.Linq;
using System.Threading.Tasks;
using Email.SendEmail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
// using Volo.Abp.BackgroundJobs.Abstractions;

public class ShapeInfoCheckWorker : AsyncPeriodicBackgroundWorkerBase
{
    // private readonly IBackgroundJobManager _backgroundJobManager;


    public ShapeInfoCheckWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        //设置23:00执行，然后间隔24小时执行
        // Set the timer period to execute the background worker every 12 hours.
        // The Timer.Period property is in milliseconds, hence the calculation:
        // 60000 milliseconds = 1 minute
        // 60 minutes = 1 hour
        // 12 hours = 12 * 60 minutes
        // Therefore, 60000 * 60 * 12 gives us the period in milliseconds for 12 hours.
        Timer.Period = 60000 * 60 * 48;
        // Timer.Period = 60000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        Logger.LogInformation("ShapeInfoCheckWorker is working.");

        var _repository = workerContext.ServiceProvider.GetRequiredService<IRepository<ShapeInfo, Guid>>();

        var _lineInfoRepository = workerContext.ServiceProvider.GetRequiredService<IRepository<LineInfo, Guid>>();

        var _sendEmailAppService = workerContext.ServiceProvider.GetRequiredService<ISendEmailAppService>();

        var lines = await _lineInfoRepository.GetListAsync();



        //取出hasError是真的或者hasChanged是真的，并且cause是空的的数据。
        var shapeInfos = await _repository.GetListAsync(a => (a.HasChanged || a.HasError) && a.Cause == null);

       
        //判断shapeInfos是否为空，不空时根据line，给PersonInCharge发邮件, 如果line 的负责人相同就放一个邮件里
        if (shapeInfos.Any())
        {
            var emailSendArgsList = shapeInfos.GroupBy(s => s.Line)
                .Select(g => new EmailSendingArgs
                {
                    // The email address of the person in charge.
                    EmailAddress = lines.First(l => l.Name == g.Key).PersonInCharge,

                    // The subject of the email.
                    Subject = "Message u need to known",

                    // The body of the email.
                    // The body will contain the following information:
                    //  - The names of the lines that the person in charge is responsible for.
                    //  - For each problematic shape info, the name, line, programId, hasError, cause, date, and id.
                    Body = $"The following are the problematic shape info for line {g.Key}:\n" +
                        string.Join("\n", g.Select(s => $"{s.Name},{s.Line},{s.ProgramId},{s.HasError},{s.HasChanged},{s.Date},{s.Id}"))
                });

            // Send the emails.
            // For each EmailSendingArgs in the list, call the _sendEmailAppService.Send method
            foreach (var emailSendArgs in emailSendArgsList)
            {
                await _sendEmailAppService.Send(emailSendArgs);
            }
        }
        
    }
}