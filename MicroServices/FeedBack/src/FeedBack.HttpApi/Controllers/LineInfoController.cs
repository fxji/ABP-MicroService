using Feedback.LineInfoManagement;
using Feedback.LineInfoManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Feedback.Controllers
{
    [RemoteService]
    [Area("Feedback")]
    [Route("api/Feedback/LineInfo")]
    [IgnoreAntiforgeryToken]
    public class LineInfoController : AbpController
    {
        private readonly ILineInfoAppService _LineInfoAppService;

        public LineInfoController(ILineInfoAppService LineInfoAppService)
        {
            _LineInfoAppService = LineInfoAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<LineInfoDto> DataPost(CreateOrUpdateLineInfoDto input)
        {
            return _LineInfoAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _LineInfoAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<LineInfoDto> Get(Guid id)
        {
            return _LineInfoAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<LineInfoDto>> GetAll(GetLineInfoInputDto input)
        {
            return _LineInfoAppService.GetAll(input);
        }
    }
}
