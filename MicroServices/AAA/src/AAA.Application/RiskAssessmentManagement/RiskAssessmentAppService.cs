using AAA.Models;
using AAA.Permissions;
using AAA.RiskAssessmentManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AAA.RiskAssessmentManagement
{
    [Authorize(AAAPermissions.A3.Default)]
    public class RiskAssessmentAppService : ApplicationService,IRiskAssessmentAppService
    {
        private const string FormName = "RiskAssessment";
        private IRepository<RiskAssessment, Guid> _repository;

        public RiskAssessmentAppService(
            IRepository<RiskAssessment, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<RiskAssessmentDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<RiskAssessment, RiskAssessmentDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<RiskAssessmentDto>> GetAll(GetRiskAssessmentInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(input.a3Id != Guid.Empty, a => a.A3Id == input.a3Id).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = query.Count();
            var items = query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

            var dto = ObjectMapper.Map<List<RiskAssessment>, List<RiskAssessmentDto>>(items);
            return new PagedResultDto<RiskAssessmentDto>(totalCount, dto);
        }

        public async Task<RiskAssessmentDto> DataPost(CreateOrUpdateRiskAssessmentDto input)
        {
            RiskAssessment result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateRiskAssessmentDto, RiskAssessment>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<RiskAssessment, RiskAssessmentDto>(result);
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