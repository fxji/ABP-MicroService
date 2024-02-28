using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AAA.IssueManagement.Dto;
using AAA.Models;
using Microsoft.AspNetCore.Authorization;
using AAA.Permissions;

namespace AAA.IssueManagement
{
    [Authorize(AAAPermissions.A3.Default)]
    public class IssueAppService : ApplicationService, IIssueAppService
    {
        private const string FormName = "Issue";
        private IRepository<Issue, Guid> _repository;

        public IssueAppService(
            IRepository<Issue, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<IssueDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Issue, IssueDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<IssueDto>> GetAll(GetIssueInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(input.a3Id != Guid.Empty, a => a.A3Id == input.a3Id).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<Issue>, List<IssueDto>>(items);
            return new PagedResultDto<IssueDto>(totalCount, dto);
        }

        public async Task<IssueDto> DataPost(CreateOrUpdateIssueDto input)
        {
            Issue result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateIssueDto, Issue>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<Issue, IssueDto>(result);
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