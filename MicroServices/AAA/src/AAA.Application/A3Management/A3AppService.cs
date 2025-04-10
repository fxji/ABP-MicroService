using AAA.A3Management.Dto;
using AAA.Models;
using AAA.Permissions;
using BaseService.BaseData.DataDictionaryManagement;
using DataExport;
using DataExport.ExportManagement;
using Email.SendEmail;
using FileStore.FileManagement;
using FileStore.FileManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;


namespace AAA.A3Management
{
    [Authorize(AAAPermissions.A3.Default)]
    public class A3AppService : ApplicationService, IA3AppService
    {
        private const string FormName = "A3";
        private IRepository<A3, Guid> _repository;
        private readonly ISendEmailAppService _sendEmailAppService;
        private readonly IDistributedEventBus _distributedEventBus;
        //private readonly A3Manager _a3Manager;
        private IRepository<Issue, Guid> _IssueRepository;
        private IRepository<RiskAssessment, Guid> _RiskAssessmentRepository;
        private IRepository<ContainmentAction, Guid> _ContainmentActionRepository;
        private IRepository<Cause, Guid> _CauseRepository;
        private IRepository<CorrectiveAction, Guid> _CorrectiveActionRepository;
        private IRepository<A3Member, Guid> _A3MemberRepository;
        private IRepository<ConfirmInfo, Guid> _ConfirmRepository;
        private IRepository<A3Attachment, Guid> _attachmentRepository;

        private IDictionaryDetailAppService _dictService;
        private IFileAppService _fileAppService;



        private readonly ServerConfigurationOptions _serverOptions;

        private readonly IExportAppService _exportService;

        //public A3AppService(
        //    IRepository<A3, Guid> repository, ISendEmailAppService sendEmailAppService, IDistributedEventBus distributedEventBus, A3Manager a3Manager,
        //    IExportAppService exportAppService
        //    )
        //{
        //    //_repository = repository; 
        //    _sendEmailAppService = sendEmailAppService; _distributedEventBus = distributedEventBus; _a3Manager = a3Manager;
        //    _exportService = exportAppService;
        //}

        public A3AppService(
            IRepository<A3, Guid> repository,
            ISendEmailAppService sendEmailAppService,
            IDistributedEventBus distributedEventBus,
            IExportAppService exportAppService,
            IRepository<Issue, Guid> issues,
            IRepository<RiskAssessment, Guid> riskAssessments,
            IRepository<ContainmentAction, Guid> containmentActions,
            IRepository<Cause, Guid> causes,
            IRepository<CorrectiveAction, Guid> correctiveActions,
            IRepository<A3Member, Guid> a3Members,
            IRepository<ConfirmInfo, Guid> confirms,
            IRepository<A3Attachment, Guid> files,
            IDictionaryDetailAppService dictionaryAppService,
            IFileAppService fileAppService,
            IOptions<ServerConfigurationOptions> options
            )
        {
            _repository = repository;
            _sendEmailAppService = sendEmailAppService;
            _distributedEventBus = distributedEventBus;
            _exportService = exportAppService;
            _IssueRepository = issues;
            _RiskAssessmentRepository = riskAssessments;
            _ContainmentActionRepository = containmentActions;
            _CauseRepository = causes;
            _CorrectiveActionRepository = correctiveActions;
            _A3MemberRepository = a3Members;
            _ConfirmRepository = confirms;
            _attachmentRepository = files;
            _dictService = dictionaryAppService;
            _fileAppService = fileAppService;
            _serverOptions = options.Value;
        }
        #region 增删改查基础方法

        public async Task<A3Dto> Get(Guid id)
        {

            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<A3, A3Dto>(data);
            return dto;

        }


        /// <summary>
        /// 新的方法
        /// https://www.jianshu.com/p/891903cd764e
        /// 直接生成FileStreamResult
        /// 避免在服务端生成文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<ExportDto> Export(Guid id)
        //{
        //    var path = await _a3Manager.Export(id);
        //    return new ExportDto()
        //    {
        //        Path ="http://localhost:44308"+ path
        //    };
        //}

        public async Task<ExportDto> Export(Guid id)
        {
            string url = "";
            var input = await CreateInputDto(id);
            var result = _exportService.Export(input);
            var excelResult = await _fileAppService.CreateFileInfoAndBlobAsync(new FileInputDto()
            {
                Name = id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx",
                Content = result,
            });
            url = _serverOptions.FileServerHost + excelResult.Url;

            return new ExportDto()
            {
                Path = url
            };
        }

        private async Task<InputExDto> CreateInputDto(Guid id)
        {
            var a3Task = _repository.GetAsync(id);
            var issuesTask = _IssueRepository.GetListAsync(i => i.A3Id == id);
            var cAtionsTask = _ContainmentActionRepository.GetListAsync(i => i.A3Id == id);
            var risksTask = _RiskAssessmentRepository.GetListAsync(i => i.A3Id == id);
            var causesTask = _CauseRepository.GetListAsync(i => i.A3Id == id);
            var correctActionsTask = _CorrectiveActionRepository.GetListAsync(i => i.A3Id == id);
            var confirmsTask = _ConfirmRepository.GetListAsync(i => i.A3Id == id);
            var filesTask = _attachmentRepository.GetListAsync(i => i.a3Id == id);

            var issueTypeQueryTask = _dictService.GetAllByDictionaryName("issueType");
            var actionTypeQueryTask = _dictService.GetAllByDictionaryName("actionType");
            var statusQueryTask = _dictService.GetAllByDictionaryName("status");
            var levelsQueryTask = _dictService.GetAllByDictionaryName("levels");
            var causeTypesQueryTask = _dictService.GetAllByDictionaryName("causeTypes");
            var causeStatusQueryTask = _dictService.GetAllByDictionaryName("causeStatus");

            await Task.WhenAll(a3Task, issuesTask, cAtionsTask, risksTask, causesTask, correctActionsTask, confirmsTask, filesTask,
                issueTypeQueryTask, actionTypeQueryTask, statusQueryTask, levelsQueryTask, causeTypesQueryTask, causeStatusQueryTask);

            var a3 = await a3Task;
            var exA3 = ObjectMapper.Map<A3, A3ExDto>(a3);

            var issues = await issuesTask;
            var cAtions = await cAtionsTask;
            var risks = await risksTask;
            var causes = await causesTask;
            var correctActions = await correctActionsTask;
            var confirms = await confirmsTask;
            var files = await filesTask;

            var issueTypeQuery = await issueTypeQueryTask;
            var actionTypeQuery = await actionTypeQueryTask;
            var statusQuery = await statusQueryTask;
            var levelsQuery = await levelsQueryTask;
            var causeTypesQuery = await causeTypesQueryTask;
            var causeStatusQuery = await causeStatusQueryTask;

            issues.ForEach(item => item.Type = issueTypeQuery.Items.First(i => i.Value == item.Type)?.Label ?? item.Type);
            cAtions.ForEach(item =>
            {
                item.Type = actionTypeQuery.Items.First(i => i.Value == item.Type)?.Label ?? item.Type;
                item.Status = statusQuery.Items.First(i => i.Value == item.Status)?.Label ?? item.Status;
            });

            risks.ForEach(item =>
            {
                item.Probability = levelsQuery.Items.First(i => i.Value == item.Probability)?.Label ?? item.Probability;
                item.Functionally = levelsQuery.Items.First(i => i.Value == item.Functionally)?.Label ?? item.Functionally;
            });

            causes.ForEach(item =>
            {
                item.Type = causeTypesQuery.Items.First(i => i.Value == item.Type)?.Label ?? item.Type;
                item.Status = causeStatusQuery.Items.First(i => i.Value == item.Status)?.Label ?? item.Status;
            });

            correctActions.ForEach(item =>
            {
                item.Status = causeStatusQuery.Items.First(i => i.Value == item.Status)?.Label ?? item.Status;
            });

            var exIssues = ObjectMapper.Map<List<Issue>, List<IssueExDto>>(issues);
            var exCations = ObjectMapper.Map<List<ContainmentAction>, List<ContainmentActionExDto>>(cAtions);
            var exRisks = ObjectMapper.Map<List<RiskAssessment>, List<RiskAssessmentExDto>>(risks);
            var exCauses = ObjectMapper.Map<List<Cause>, List<CauseExDto>>(causes);
            var exCorrectActions = ObjectMapper.Map<List<CorrectiveAction>, List<CorrectiveActionExDto>>(correctActions);
            var exConfirms = ObjectMapper.Map<List<ConfirmInfo>, List<ConfirmInfoExDto>>(confirms);

            var imageTasks = files.Where(f => f.Type == "Image").Select(async item =>
            {
                var content = await _fileAppService.GetFileContentAsyncByName(item.Name);
                return new A3AttachmentExDto { Content = content };
            });

            var images = await Task.WhenAll(imageTasks);

            return new InputExDto
            {
                A3 = exA3,
                Issues = exIssues,
                ContainmentActions = exCations,
                RiskAssessments = exRisks,
                Causes = exCauses,
                CorrectiveActions = exCorrectActions,
                ConfirmInfos = exConfirms,
                A3Attachments = images.ToList(),
            };
        }

        public async Task<PagedResultDto<A3Dto>> GetAll(GetA3InputDto input)
        {
            var query = (await _repository.GetQueryableAsync())

                .WhereIf(!(input.A3Id == Guid.Empty || input.A3Id is null), a => a.Id.Equals(input.A3Id))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Source), a => a.Source.Equals(input.Source))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Process), a => a.Process.Equals(input.Process))
                .WhereIf(!(input.OrganizationId == Guid.Empty || input.OrganizationId is null), a => a.OrganizationId.Equals(input.OrganizationId))
                ;

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

            var dto = ObjectMapper.Map<List<A3>, List<A3Dto>>(items);
            return new PagedResultDto<A3Dto>(totalCount, dto);
        }

        public async Task<A3Dto> DataPost(CreateOrUpdateA3Dto input)
        {
            A3 result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateA3Dto, A3>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }



            return ObjectMapper.Map<A3, A3Dto>(result);
        }

        public async Task Share(ShareDto input)
        {

            await _sendEmailAppService.Send(
                 new EmailSendingArgs
                 {
                     EmailAddress = input.EmailAddress,
                     Subject = "Message u need to known",
                     Body = $"A3Report {input.A3.Name} was created, pls check {_serverOptions.FileServerHost}/#/A3?a3id={input.A3.Id} and confirm"
                 }
            );

            //await _distributedEventBus.PublishAsync(
            //    new StockCountChangedEto
            //    {
            //        ProductId = productId,
            //        NewCount = newCount
            //    }
            //);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }
        }


        #endregion

    }
}