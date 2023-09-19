using AAA.CorrectiveActionManagement;
using AAA.CorrectiveActionManagement.Dto;
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
    [Route("api/app/CorrectiveAction")]
    public class CorrectiveActionController : AbpController
    {
        private readonly ICorrectiveActionAppService _CorrectiveActionAppService;

        public CorrectiveActionController(ICorrectiveActionAppService CorrectiveActionAppService)
        {
            _CorrectiveActionAppService = CorrectiveActionAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<CorrectiveActionDto> DataPost(CreateOrUpdateCorrectiveActionDto input)
        {
            return _CorrectiveActionAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _CorrectiveActionAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CorrectiveActionDto> Get(Guid id)
        {
            return _CorrectiveActionAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<CorrectiveActionDto>> GetAll(GetCorrectiveActionInputDto input)
        {
            return _CorrectiveActionAppService.GetAll(input);
        }
    }
}
