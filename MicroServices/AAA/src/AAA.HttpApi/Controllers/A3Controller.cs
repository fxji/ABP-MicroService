using AAA.A3Management;
using AAA.A3Management.Dto;
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
    [Route("api/AAA/A3")]
    public class A3Controller : AbpController
    {
        private readonly IA3AppService _A3AppService;

        public A3Controller(IA3AppService A3AppService)
        {
            _A3AppService = A3AppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<A3Dto> DataPost(CreateOrUpdateA3Dto input)
        {
            return _A3AppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _A3AppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<A3Dto> Get(Guid id)
        {
            return _A3AppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<A3Dto>> GetAll(GetA3InputDto input)
        {
            return _A3AppService.GetAll(input);
        }
    }
}
