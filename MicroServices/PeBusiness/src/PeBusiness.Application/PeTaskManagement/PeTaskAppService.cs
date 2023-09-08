using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using PeBusiness.PeTaskManagement.Dto;
using PeBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using PeBusiness.Permissions;

namespace PeBusiness.PeTaskManagement
{
    [Authorize(PeBusinessPermissions.PeTask.Default)]
    public class PeTaskAppService : ApplicationService,IPeTaskAppService
    {
        private const string FormName = "PeTask";
        private IRepository<PeTask, Guid> _repository;

        public PeTaskAppService(
            IRepository<PeTask, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<PeTaskDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<PeTask, PeTaskDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<PeTaskDto>> GetAll(GetPeTaskInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<PeTask>, List<PeTaskDto>>(items);
            return new PagedResultDto<PeTaskDto>(totalCount, dto);
        }

        public async Task<PeTaskDto> DataPost(CreateOrUpdatePeTaskDto input)
        {
            PeTask result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdatePeTaskDto, PeTask>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<PeTask, PeTaskDto>(result);
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