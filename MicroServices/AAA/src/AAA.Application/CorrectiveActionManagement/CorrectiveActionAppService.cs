using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AAA.CorrectiveActionManagement.Dto;
using AAA.Models;
using Microsoft.AspNetCore.Authorization;
using AAA.Permissions;

namespace AAA.CorrectiveActionManagement
{
    [Authorize(AAAPermissions.CorrectiveAction.Default)]
    public class CorrectiveActionAppService : ApplicationService,ICorrectiveActionAppService
    {
        private const string FormName = "CorrectiveAction";
        private IRepository<CorrectiveAction, Guid> _repository;

        public CorrectiveActionAppService(
            IRepository<CorrectiveAction, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<CorrectiveActionDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<CorrectiveAction, CorrectiveActionDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<CorrectiveActionDto>> GetAll(GetCorrectiveActionInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(input.a3Id != Guid.Empty, a => a.A3Id == input.a3Id).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<CorrectiveAction>, List<CorrectiveActionDto>>(items);
            return new PagedResultDto<CorrectiveActionDto>(totalCount, dto);
        }

        public async Task<CorrectiveActionDto> DataPost(CreateOrUpdateCorrectiveActionDto input)
        {
            CorrectiveAction result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateCorrectiveActionDto, CorrectiveAction>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<CorrectiveAction, CorrectiveActionDto>(result);
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