using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using A3.Confirm.ConfirmManagement.Dto;
using A3.Confirm.Models;
using Microsoft.AspNetCore.Authorization;
using A3.Confirm.Permissions;

namespace A3.Confirm.ConfirmManagement
{
    [Authorize(A3.ConfirmPermissions.Confirm.Default)]
    public class ConfirmAppService : ApplicationService,IConfirmAppService
    {
        private const string FormName = "ConfirmInfo";
        private IRepository<Confirm, Guid> _repository;

        public ConfirmAppService(
            IRepository<Confirm, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<ConfirmDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Confirm, ConfirmDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<ConfirmDto>> GetAll(GetConfirmInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<Confirm>, List<ConfirmDto>>(items);
            return new PagedResultDto<ConfirmDto>(totalCount, dto);
        }

        public async Task<ConfirmDto> DataPost(CreateOrUpdateConfirmDto input)
        {
            Confirm result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateConfirmDto, Confirm>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<Confirm, ConfirmDto>(result);
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