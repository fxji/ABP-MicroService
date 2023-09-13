using AAA.IssueManagement;
using AAA.IssueManagement.Dto;
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
    [Route("api/AAA/Issue")]
    public class IssueController : AbpController
    {
        private readonly IIssueAppService _IssueAppService;

        public IssueController(IIssueAppService IssueAppService)
        {
            _IssueAppService = IssueAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<IssueDto> DataPost(CreateOrUpdateIssueDto input)
        {
            return _IssueAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _IssueAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IssueDto> Get(Guid id)
        {
            return _IssueAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<IssueDto>> GetAll(GetIssueInputDto input)
        {
            return _IssueAppService.GetAll(input);
        }
    }
}
