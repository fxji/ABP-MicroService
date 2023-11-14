using AAA.CauseManagement;
using AAA.CauseManagement.Dto;
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
    [Route("api/AAA/Cause")]
    public class CauseController : AbpController
    {
        private readonly ICauseAppService _CauseAppService;

        public CauseController(ICauseAppService CauseAppService)
        {
            _CauseAppService = CauseAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<CauseDto> DataPost(CreateOrUpdateCauseDto input)
        {
            return _CauseAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _CauseAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CauseDto> Get(Guid id)
        {
            return _CauseAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<CauseDto>> GetAll(GetCauseInputDto input)
        {
            return _CauseAppService.GetAll(input);
        }
    }
}
