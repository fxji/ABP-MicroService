using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Feedback.ShapeInfoManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using FeedBack.Permissions;

namespace Feedback.ShapeInfoManagement
{
    [Authorize(FeedBackPermissions.ShapeInfo.Default)]
    public class ShapeInfoAppService : ApplicationService, IShapeInfoAppService
    {
        private const string FormName = "ShapeInfo";
        private IRepository<ShapeInfo, Guid> _repository;

        private IRepository<ProgramInfo, Guid> _programInfoRepository;

        public ShapeInfoAppService(
            IRepository<ShapeInfo, Guid> repository,
            IRepository<ProgramInfo, Guid> programInfoRepository
            )
        {
            _repository = repository;
            _programInfoRepository = programInfoRepository;
        }
        #region 增删改查基础方法

        public async Task<ShapeInfoDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<ShapeInfo, ShapeInfoDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<ShapeInfoDto>> GetAll(GetShapeInfoInputDto input)
        {
            var query = (await _repository.GetQueryableAsync())
            .WhereIf(input.ProgramId.HasValue, a => a.ProgramId == input.ProgramId)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Name), a => a.Name.Contains(input.Name))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Line), a => a.Line.Contains(input.Line))
            .WhereIf(input.HasError.HasValue, a => a.HasError == input.HasError)
            .WhereIf(input.HasChanged.HasValue, a => a.HasChanged == input.HasChanged)
            ;

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Date desc")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();



            var programIds = items.Select(i => i.ProgramId).Distinct().ToList();
            var programs = await _programInfoRepository.GetListAsync(p => programIds.Contains(p.Id));
            var programDict = programs.ToDictionary(p => p.Id, p => p.Name);

            var dto = items.Select(item =>
            {
                var shapeInfoDto = ObjectMapper.Map<ShapeInfo, ShapeInfoDto>(item);
                shapeInfoDto.ProgramName = programDict[item.ProgramId];
                return shapeInfoDto;
            }).ToList();

            return new PagedResultDto<ShapeInfoDto>(totalCount, dto);
        }

        public async Task<ShapeInfoDto> DataPost(CreateOrUpdateShapeInfoDto input)
        {
            ShapeInfo result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateShapeInfoDto, ShapeInfo>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<ShapeInfo, ShapeInfoDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }

        }

        public async Task<ShapeInfoDto> GetLatestByNameAndLine(GetShapeInfoInputDto input)
        {
            var query = (await _repository.GetQueryableAsync())
                .Where(a => a.Name == input.Name && a.Line == input.Line)
                .OrderByDescending(a => a.CreationTime)
                .FirstOrDefault();

            return ObjectMapper.Map<ShapeInfo, ShapeInfoDto>(query);
        }

        public async Task<List<ShapeInfoDto>> BatchUpdateCauseAsync(BatchUpdateShapeInfoDto input)
        {
            var shapeInfos = await _repository.GetListAsync(a => input.Ids.Contains(a.Id));

            foreach (var shapeInfo in shapeInfos)
            {
                shapeInfo.Cause = input.Cause;
            }

            await _repository.UpdateManyAsync(shapeInfos);

            return shapeInfos
        .Select(x => new ShapeInfoDto
        {
            Id = x.Id,
            Cause = x.Cause
            // ...其他字段
        }).ToList();
        }


        #endregion

    }
}
