using AAA.A3AttachmentManagement;
using AAA.A3AttachmentManagement.Dto;
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
    [Route("api/AAA/A3Attachment")]
    public class A3AttachmentController : AbpController
    {
        private readonly IA3AttachmentAppService _A3AttachmentAppService;

        public A3AttachmentController(IA3AttachmentAppService A3AttachmentAppService)
        {
            _A3AttachmentAppService = A3AttachmentAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<A3AttachmentDto> DataPost(CreateOrUpdateA3AttachmentDto input)
        {
            return _A3AttachmentAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _A3AttachmentAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<A3AttachmentDto> Get(Guid id)
        {
            return _A3AttachmentAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<A3AttachmentDto>> GetAll(GetA3AttachmentInputDto input)
        {
            return _A3AttachmentAppService.GetAll(input);
        }
    }
}
