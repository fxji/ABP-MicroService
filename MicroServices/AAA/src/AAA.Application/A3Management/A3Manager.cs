using AAA.Models;
using BaseService.BaseData.DataDictionaryManagement;
using DataExport.Export;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace AAA.A3Management
{
    public class A3Manager : DomainService
    {
        private IRepository<A3, Guid> _A3Repository;
        private IRepository<Issue, Guid> _IssueRepository;
        private IRepository<RiskAssessment, Guid> _RiskAssessmentRepository;
        private IRepository<ContainmentAction, Guid> _ContainmentActionRepository;
        private IRepository<Cause, Guid> _CauseRepository;
        private IRepository<CorrectiveAction, Guid> _CorrectiveActionRepository;
        private IRepository<A3Member, Guid> _A3MemberRepository;
        private IRepository<ConfirmInfo, Guid> _ConfirmRepository;
        private IRepository<A3Attachment, Guid> _attachmentRepository;

        private IDictionaryDetailAppService _dictService;


        private readonly ExportAppService _exportService;

        private readonly ServerConfigurationOptions _serverOptions;


        public string FrontEndUrl { get { return _serverOptions.FileServerHost; } }


        public A3Manager(
            IRepository<A3, Guid> a3s,
            IRepository<Issue, Guid> issues,
            IRepository<RiskAssessment, Guid> riskAssessments,
            IRepository<ContainmentAction, Guid> containmentActions,
            IRepository<Cause, Guid> causes,
            IRepository<CorrectiveAction, Guid> correctiveActions,
            IRepository<A3Member, Guid> a3Members,
            IRepository<ConfirmInfo, Guid> confirms,
            IRepository<A3Attachment, Guid> files,
            ExportAppService exportAppService,
            IDictionaryDetailAppService dictionaryAppService,
            IOptions<ServerConfigurationOptions> options
            )
        {
            _A3Repository = a3s;
            _IssueRepository = issues;
            _RiskAssessmentRepository = riskAssessments;
            _ContainmentActionRepository = containmentActions;
            _CauseRepository = causes;
            _CorrectiveActionRepository = correctiveActions;
            _A3MemberRepository = a3Members;
            _ConfirmRepository = confirms;
            _attachmentRepository = files;
            _exportService = exportAppService;
            _dictService = dictionaryAppService;
            _serverOptions = options.Value;
        }


        public async Task<string> Export(Guid id)
        {
            string result;
            var a3 = await _A3Repository.GetAsync(id);

            List<Issue> issues = await _IssueRepository.GetListAsync(i => i.A3Id == id);
            List<ContainmentAction> cAtions = await _ContainmentActionRepository.GetListAsync(i => i.A3Id == id);
            List<RiskAssessment> risks = await _RiskAssessmentRepository.GetListAsync(i => i.A3Id == id);
            List<Cause> causes = await _CauseRepository.GetListAsync(i => i.A3Id == id);
            List<CorrectiveAction> correctActions = await _CorrectiveActionRepository.GetListAsync(i => i.A3Id == id);
            List<ConfirmInfo> confirms = await _ConfirmRepository.GetListAsync(i => i.A3Id == id);
            List<A3Attachment> files = await _attachmentRepository.GetListAsync(i => i.a3Id == id);

            _exportService.OpenExcel();
            _exportService.WriteHeader(a3.Id.ToString(), a3.Name);


            if (issues.Count > 0)
            {

                var item = issues[0];

                var query = await _dictService.GetAllByDictionaryName("issueType");
                var typeName = query.Items.First(i => i.Value == item.Type).Label;

                _exportService.WriteIssue(
                    item.Name,
                    item.CustomerGroup,
                    item.Description,
                    item.OccurrenceDate.Value,
                    item.Project,
                    typeName,
                    item.Rate,
                    item.SymptomDescription,
                    item.GoalStatement
                    );
            }

            foreach (var item in cAtions)
            {
                var query = await _dictService.GetAllByDictionaryName("actionType");
                var typeName = query.Items.First(i => i.Value == item.Type).Label;

                var query2 = await _dictService.GetAllByDictionaryName("status");
                var statusName = query2.Items.First(i => i.Value == item.Status).Label;

                _exportService.WriteContainmentAction(typeName, item.Name, item.Responsibility, statusName);

            }

            foreach (var item in risks)
            {
                var query = await _dictService.GetAllByDictionaryName("levels");
                var probabilityName = query.Items.First(i => i.Value == item.Probability).Label;
                var functionallyName = query.Items.First(i => i.Value == item.Functionally).Label;

                _exportService.WriteRisk(item.Name, item.Description, probabilityName, functionallyName, item.SafetyRelevant);
            }

            foreach (var item in causes)
            {

                var query = await _dictService.GetAllByDictionaryName("causeTypes");
                var typeName = query.Items.First(i => i.Value == item.Type).Label;

                var query2 = await _dictService.GetAllByDictionaryName("causeStatus");
                var statusName = query2.Items.First(i => i.Value == item.Status).Label;

                _exportService.WriteCause(typeName, item.Name, statusName);
            }

            foreach (var item in correctActions)
            {
                var query2 = await _dictService.GetAllByDictionaryName("causeStatus");
                var statusName = query2.Items.First(i => i.Value == item.Status).Label;

                _exportService.WriteCorrectiveActions(item.Name, item.Responsibility, item.DueDate, statusName);
            }

            foreach (var item in confirms)
            {
                _exportService.WriteReadAcross(item.Comments, item.CreatorId.Value.ToString(), item.CreationTime);
            }

            _exportService.WriteFooter(a3.OrganizationId.ToString(), a3.UserEmail, a3.StartDate);

            foreach (var item in files)
            {
                if (item.Type == "Image")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync(_serverOptions.FileServerHost + item.Url))
                        {
                            response.EnsureSuccessStatusCode();


                            var contentByte = await response.Content.ReadAsByteArrayAsync();

                            _exportService.WriteImage(contentByte);

                        }
                    }
                    //var list = await _fileAppService.GetAll(new GetFileInputDto());
                }
            }

            result = _exportService.Export(a3.Name);

            return result;
        }


        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _A3Repository.DeleteAsync(item);
            }

        }

        public async Task<A3> Insert(A3 input)
        {
            return await _A3Repository.InsertAsync(input);

        }


        public async Task<A3> Update(A3 input)
        {
            return await _A3Repository.UpdateAsync(input);
        }

        public async Task<A3> GetAsync(Guid value)
        {
            return await _A3Repository.GetAsync(value);
        }


        public async Task<IQueryable<A3>> GetAll()
        {
            return await _A3Repository.GetQueryableAsync();
        }
    }
}
