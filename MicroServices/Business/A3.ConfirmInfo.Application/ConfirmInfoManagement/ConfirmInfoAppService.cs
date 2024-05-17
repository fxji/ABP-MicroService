using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using A3.ConfirmInfo.ConfirmInfoManagement.Dto;
using A3.ConfirmInfo.Models;
using Microsoft.AspNetCore.Authorization;
using A3.ConfirmInfo.Permissions;

namespace A3.ConfirmInfo.ConfirmInfoManagement
{
    [Authorize(A3.ConfirmInfoPermissions.ConfirmInfo.Default)]
    public class ConfirmInfoAppService : ApplicationService,IConfirmInfoAppService
    {
        private const string FormName = "ConfirmInfo";
        private IRepository<ConfirmInfo, Guid> _repository;

        public ConfirmInfoAppService(
            IRepository<ConfirmInfo, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<ConfirmInfoDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<ConfirmInfo, ConfirmInfoDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<ConfirmInfoDto>> GetAll(GetConfirmInfoInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<ConfirmInfo>, List<ConfirmInfoDto>>(items);
            return new PagedResultDto<ConfirmInfoDto>(totalCount, dto);
        }

        public async Task<ConfirmInfoDto> DataPost(CreateOrUpdateConfirmInfoDto input)
        {
            ConfirmInfo result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateConfirmInfoDto, ConfirmInfo>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<ConfirmInfo, ConfirmInfoDto>(result);
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