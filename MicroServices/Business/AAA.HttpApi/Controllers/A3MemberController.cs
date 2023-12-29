using AAA.A3MemberManagement;
using AAA.A3MemberManagement.Dto;
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
    [Route("api/app/A3Member")]
    public class A3MemberController : AbpController
    {
        private readonly IA3MemberAppService _A3MemberAppService;

        public A3MemberController(IA3MemberAppService A3MemberAppService)
        {
            _A3MemberAppService = A3MemberAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<A3MemberDto> DataPost(CreateOrUpdateA3MemberDto input)
        {
            return _A3MemberAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _A3MemberAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<A3MemberDto> Get(Guid id)
        {
            return _A3MemberAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<A3MemberDto>> GetAll(GetA3MemberInputDto input)
        {
            return _A3MemberAppService.GetAll(input);
        }
    }
}
