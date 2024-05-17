using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AAA.A3AttachmentManagement.Dto;
using AAA.Models;
using Microsoft.AspNetCore.Authorization;
using AAA.Permissions;

namespace AAA.A3AttachmentManagement
{
    [Authorize(AAAPermissions.A3.Default)]
    public class A3AttachmentAppService : ApplicationService,IA3AttachmentAppService
    {
        private const string FormName = "A3Attachment";
        private IRepository<A3Attachment, Guid> _repository;

        public A3AttachmentAppService(
            IRepository<A3Attachment, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<A3AttachmentDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<A3Attachment, A3AttachmentDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<A3AttachmentDto>> GetAll(GetA3AttachmentInputDto input)
        {
            var query = (
                await _repository.GetQueryableAsync())
                .WhereIf(input.A3Id!=Guid.Empty, a => a.a3Id == input.A3Id)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Type), a => a.Type.Equals(input.Type))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Equals(input.Filter)
                );

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<A3Attachment>, List<A3AttachmentDto>>(items);
            return new PagedResultDto<A3AttachmentDto>(totalCount, dto);
        }

        public async Task<A3AttachmentDto> DataPost(CreateOrUpdateA3AttachmentDto input)
        {
            A3Attachment result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateA3AttachmentDto, A3Attachment>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<A3Attachment, A3AttachmentDto>(result);
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