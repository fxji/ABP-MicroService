using PeBusiness.PeTaskManagement;
using PeBusiness.PeTaskManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace PeBusiness.Controllers
{
    [RemoteService]
    [Area("PeBusiness")]
    [Route("api/app/PeTask")]
    public class PeTaskController : AbpController
    {
        private readonly IPeTaskAppService _PeTaskAppService;

        public PeTaskController(IPeTaskAppService PeTaskAppService)
        {
            _PeTaskAppService = PeTaskAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<PeTaskDto> DataPost(CreateOrUpdatePeTaskDto input)
        {
            return _PeTaskAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _PeTaskAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<PeTaskDto> Get(Guid id)
        {
            return _PeTaskAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<PeTaskDto>> GetAll(GetPeTaskInputDto input)
        {
            return _PeTaskAppService.GetAll(input);
        }
    }
}
