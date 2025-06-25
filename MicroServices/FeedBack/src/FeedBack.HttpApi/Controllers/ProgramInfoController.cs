using FeedBack.ProgramInfoManagement;
using FeedBack.ProgramInfoManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace FeedBack.Controllers
{
    [RemoteService(Name = FeedBackRemoteServiceConsts.RemoteServiceName)]
    [Area(FeedBackRemoteServiceConsts.ModuleName)]
    [Route("api/FeedBack/ProgramInfo")]
    public class ProgramInfoController : AbpController, IProgramInfoAppService
    {
        private readonly IProgramInfoAppService _ProgramInfoAppService;

        public ProgramInfoController(IProgramInfoAppService ProgramInfoAppService)
        {
            _ProgramInfoAppService = ProgramInfoAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<ProgramInfoDto> DataPost(CreateOrUpdateProgramInfoDto input)
        {
            return _ProgramInfoAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _ProgramInfoAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProgramInfoDto> Get(Guid id)
        {
            return _ProgramInfoAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProgramInfoDto>> GetAll(GetProgramInfoInputDto input)
        {
            return _ProgramInfoAppService.GetAll(input);
        }

        [HttpGet]
        [Route("get-latest-by-name-and-line")]
        public Task<ProgramInfoDto> GetLatestByNameAndLine(GetProgramInfoInputDto input)
        {
            return _ProgramInfoAppService.GetLatestByNameAndLine(input);
        }
    }
}
