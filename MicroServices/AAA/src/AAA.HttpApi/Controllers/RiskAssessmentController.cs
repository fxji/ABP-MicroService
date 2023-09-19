using AAA.RiskAssessmentManagement;
using AAA.RiskAssessmentManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AAA.Controllers
{
    [RemoteService]
    [Area("AAA")]
    [Route("api/app/RiskAssessment")]
    public class RiskAssessmentController : AbpController
    {
        private readonly IRiskAssessmentAppService _RiskAssessmentAppService;

        public RiskAssessmentController(IRiskAssessmentAppService RiskAssessmentAppService)
        {
            _RiskAssessmentAppService = RiskAssessmentAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<RiskAssessmentDto> DataPost(CreateOrUpdateRiskAssessmentDto input)
        {
            return _RiskAssessmentAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _RiskAssessmentAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<RiskAssessmentDto> Get(Guid id)
        {
            return _RiskAssessmentAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<RiskAssessmentDto>> GetAll(GetRiskAssessmentInputDto input)
        {
            return _RiskAssessmentAppService.GetAll(input);
        }
    }
}
