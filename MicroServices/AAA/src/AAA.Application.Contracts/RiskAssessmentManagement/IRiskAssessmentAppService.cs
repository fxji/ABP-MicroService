using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.RiskAssessmentManagement.Dto;

namespace AAA.RiskAssessmentManagement
{
    public interface IRiskAssessmentAppService : IApplicationService
    {
        Task<RiskAssessmentDto> Get(Guid id);

        Task<PagedResultDto<RiskAssessmentDto>> GetAll(GetRiskAssessmentInputDto input);

        Task<RiskAssessmentDto> DataPost(CreateOrUpdateRiskAssessmentDto input);

        Task Delete(List<Guid> ids);
    }
}
