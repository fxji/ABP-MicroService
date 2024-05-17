using A3.Confirm.ConfirmManagement;
using A3.Confirm.ConfirmManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace A3.Confirm.Controllers
{
    [RemoteService]
    [Area("A3.Confirm")]
    [Route("api/app/Confirm")]
    public class ConfirmController : AbpController
    {
        private readonly IConfirmAppService _ConfirmAppService;

        public ConfirmController(IConfirmAppService ConfirmAppService)
        {
            _ConfirmAppService = ConfirmAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<ConfirmDto> DataPost(CreateOrUpdateConfirmDto input)
        {
            return _ConfirmAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _ConfirmAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ConfirmDto> Get(Guid id)
        {
            return _ConfirmAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ConfirmDto>> GetAll(GetConfirmInputDto input)
        {
            return _ConfirmAppService.GetAll(input);
        }
    }
}
