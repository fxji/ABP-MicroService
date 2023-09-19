using AAA.ContainmentActionManagement;
using AAA.ContainmentActionManagement.Dto;
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
    [Route("api/app/ContainmentAction")]
    public class ContainmentActionController : AbpController
    {
        private readonly IContainmentActionAppService _ContainmentActionAppService;

        public ContainmentActionController(IContainmentActionAppService ContainmentActionAppService)
        {
            _ContainmentActionAppService = ContainmentActionAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<ContainmentActionDto> DataPost(CreateOrUpdateContainmentActionDto input)
        {
            return _ContainmentActionAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _ContainmentActionAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ContainmentActionDto> Get(Guid id)
        {
            return _ContainmentActionAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ContainmentActionDto>> GetAll(GetContainmentActionInputDto input)
        {
            return _ContainmentActionAppService.GetAll(input);
        }
    }
}
