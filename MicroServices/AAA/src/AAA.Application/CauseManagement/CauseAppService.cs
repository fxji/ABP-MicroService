using AAA.CauseManagement.Dto;
using AAA.Models;
using AAA.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AAA.CauseManagement
{
    [Authorize(AAAPermissions.A3.Default)]
    public class CauseAppService : ApplicationService,ICauseAppService
    {
        private const string FormName = "Cause";
        private IRepository<Cause, Guid> _repository;

        public CauseAppService(
            IRepository<Cause, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<CauseDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Cause, CauseDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<CauseDto>> GetAll(GetCauseInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(input.a3Id != Guid.Empty, a => a.A3Id == input.a3Id).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

            var dto = ObjectMapper.Map<List<Cause>, List<CauseDto>>(items);
            return new PagedResultDto<CauseDto>(totalCount, dto);
        }

        public async Task<CauseDto> DataPost(CreateOrUpdateCauseDto input)
        {
            Cause result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateCauseDto, Cause>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<Cause, CauseDto>(result);
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