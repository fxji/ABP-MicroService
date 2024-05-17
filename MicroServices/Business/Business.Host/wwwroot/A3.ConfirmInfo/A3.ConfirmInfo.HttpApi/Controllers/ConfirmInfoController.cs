using A3.ConfirmInfo.ConfirmInfoManagement;
using A3.ConfirmInfo.ConfirmInfoManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace A3.ConfirmInfo.Controllers
{
    [RemoteService]
    [Area("A3.ConfirmInfo")]
    [Route("api/app/ConfirmInfo")]
    public class ConfirmInfoController : AbpController
    {
        private readonly IConfirmInfoAppService _ConfirmInfoAppService;

        public ConfirmInfoController(IConfirmInfoAppService ConfirmInfoAppService)
        {
            _ConfirmInfoAppService = ConfirmInfoAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<ConfirmInfoDto> DataPost(CreateOrUpdateConfirmInfoDto input)
        {
            return _ConfirmInfoAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _ConfirmInfoAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ConfirmInfoDto> Get(Guid id)
        {
            return _ConfirmInfoAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ConfirmInfoDto>> GetAll(GetConfirmInfoInputDto input)
        {
            return _ConfirmInfoAppService.GetAll(input);
        }
    }
}
