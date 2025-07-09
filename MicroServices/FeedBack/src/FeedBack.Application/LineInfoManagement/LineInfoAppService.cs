using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Feedback.LineInfoManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using FeedBack.Permissions;

namespace Feedback.LineInfoManagement
{
    [Authorize]
    public class LineInfoAppService : ApplicationService, ILineInfoAppService
    {
        private const string FormName = "产线负责人";
        private IRepository<LineInfo, Guid> _repository;

        public LineInfoAppService(
            IRepository<LineInfo, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<LineInfoDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<LineInfo, LineInfoDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<LineInfoDto>> GetAll(GetLineInfoInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

            var dto = ObjectMapper.Map<List<LineInfo>, List<LineInfoDto>>(items);
            return new PagedResultDto<LineInfoDto>(totalCount, dto);
        }

        public async Task<LineInfoDto> DataPost(CreateOrUpdateLineInfoDto input)
        {
            LineInfo result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateLineInfoDto, LineInfo>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<LineInfo, LineInfoDto>(result);
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