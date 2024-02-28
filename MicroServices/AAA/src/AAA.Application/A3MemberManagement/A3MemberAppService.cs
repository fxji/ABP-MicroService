using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AAA.A3MemberManagement.Dto;
using AAA.Models;
using Microsoft.AspNetCore.Authorization;
using AAA.Permissions;

namespace AAA.A3MemberManagement
{
    [Authorize(AAAPermissions.A3.Default)]
    public class A3MemberAppService : ApplicationService,IA3MemberAppService
    {
        private const string FormName = "A3Member";
        private IRepository<A3Member,Guid> _repository;

        public A3MemberAppService(
            IRepository<A3Member, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<A3MemberDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<A3Member, A3MemberDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<A3MemberDto>> GetAll(GetA3MemberInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(input.a3Id != Guid.Empty, a => a.A3Id == input.a3Id).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.UserId.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<A3Member>, List<A3MemberDto>>(items);
            return new PagedResultDto<A3MemberDto>(totalCount, dto);
        }

        public async Task<A3MemberDto> DataPost(CreateOrUpdateA3MemberDto input)
        {
            A3Member result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateA3MemberDto, A3Member>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<A3Member, A3MemberDto>(result);
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