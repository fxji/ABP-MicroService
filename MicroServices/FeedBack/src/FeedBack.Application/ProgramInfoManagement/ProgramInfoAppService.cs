using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using FeedBack.ProgramInfoManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using FeedBack.Permissions;

namespace FeedBack.ProgramInfoManagement
{
    [Authorize(FeedBackPermissions.ProgramInfo.Default)]
    public class ProgramInfoAppService : ApplicationService, IProgramInfoAppService
    {
        private const string FormName = "ProgramInfo";
        private IRepository<ProgramInfo, Guid> _repository;

        public ProgramInfoAppService(
            IRepository<ProgramInfo, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<ProgramInfoDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<ProgramInfo, ProgramInfoDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<ProgramInfoDto>> GetAll(GetProgramInfoInputDto input)
        {
            var query = (await _repository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrWhiteSpace(input.Name), a => a.Name.Contains(input.Name))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Line), a => a.Line.Equals(input.Line))
            .WhereIf(input.StartDate.HasValue && input.EndDate.HasValue, a => a.Date >= input.StartDate && a.Date <= input.EndDate);

            // var query = (await _repository.WithDetailsAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Date desc")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

            ObjectMapper.Map<List<ProgramInfo>, List<ProgramInfoDto>>(items);

            var dto = ObjectMapper.Map<List<ProgramInfo>, List<ProgramInfoDto>>(items);
            return new PagedResultDto<ProgramInfoDto>(totalCount, dto);
        }

        public async Task<ProgramInfoDto> DataPost(CreateOrUpdateProgramInfoDto input)
        {
            ProgramInfo result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateProgramInfoDto, ProgramInfo>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<ProgramInfo, ProgramInfoDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }

        }

        public async Task<ProgramInfoDto> GetLatestByNameAndLine(GetProgramInfoInputDto input)
        {
            var query = await _repository.GetQueryableAsync();

            var latestItem = query
                .Where(a => a.Name == input.Name && a.Line == input.Line)
                .OrderByDescending(a => a.CreationTime)
                .FirstOrDefault();

            return ObjectMapper.Map<ProgramInfo, ProgramInfoDto>(latestItem);
        }


        #endregion

    }
}