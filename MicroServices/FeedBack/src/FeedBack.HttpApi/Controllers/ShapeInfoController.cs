using Feedback.ShapeInfoManagement;
using Feedback.ShapeInfoManagement.Dto;
using FeedBack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Feedback.Controllers
{
    [RemoteService(Name = FeedBackRemoteServiceConsts.RemoteServiceName)]
    [Area(FeedBackRemoteServiceConsts.ModuleName)]
    [Route("api/FeedBack/ShapeInfo")]
    public class ShapeInfoController : AbpController, IShapeInfoAppService
    {
        private readonly IShapeInfoAppService _ShapeInfoAppService;

        public ShapeInfoController(IShapeInfoAppService ShapeInfoAppService)
        {
            _ShapeInfoAppService = ShapeInfoAppService;
        }


        [HttpPatch]
        [Route("batch-update-cause")]
        public Task<List<ShapeInfoDto>> BatchUpdateCauseAsync(BatchUpdateShapeInfoDto input)
        {
            return _ShapeInfoAppService.BatchUpdateCauseAsync(input);
        }

        [HttpPost]
        [Route("data-post")]
        public Task<ShapeInfoDto> DataPost(CreateOrUpdateShapeInfoDto input)
        {
            return _ShapeInfoAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _ShapeInfoAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ShapeInfoDto> Get(Guid id)
        {
            return _ShapeInfoAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ShapeInfoDto>> GetAll(GetShapeInfoInputDto input)
        {
            return _ShapeInfoAppService.GetAll(input);
        }

        [HttpGet]
        [Route("get-latest-by-name-and-line")]
        public Task<ShapeInfoDto> GetLatestByNameAndLine(GetShapeInfoInputDto input)
        {
            return _ShapeInfoAppService.GetLatestByNameAndLine(input);
        }
    }
}
