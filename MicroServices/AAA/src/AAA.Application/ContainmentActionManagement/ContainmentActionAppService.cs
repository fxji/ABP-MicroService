using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AAA.ContainmentActionManagement.Dto;
using AAA.Models;
using Microsoft.AspNetCore.Authorization;
using AAA.Permissions;

namespace AAA.ContainmentActionManagement
{
    [Authorize(AAAPermissions.ContainmentAction.Default)]
    public class ContainmentActionAppService : ApplicationService,IContainmentActionAppService
    {
        private const string FormName = "ContainmentAction";
        private IRepository<ContainmentAction, Guid> _repository;

        public ContainmentActionAppService(
            IRepository<ContainmentAction, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<ContainmentActionDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<ContainmentAction, ContainmentActionDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<ContainmentActionDto>> GetAll(GetContainmentActionInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Activities.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<ContainmentAction>, List<ContainmentActionDto>>(items);
            return new PagedResultDto<ContainmentActionDto>(totalCount, dto);
        }

        public async Task<ContainmentActionDto> DataPost(CreateOrUpdateContainmentActionDto input)
        {
            ContainmentAction result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateContainmentActionDto, ContainmentAction>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<ContainmentAction, ContainmentActionDto>(result);
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