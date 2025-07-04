using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Feedback.ShapeInfoManagement.Dto;

namespace Feedback.ShapeInfoManagement
{
    public interface IShapeInfoAppService : IApplicationService
    {
        Task<ShapeInfoDto> Get(Guid id);

        Task<PagedResultDto<ShapeInfoDto>> GetAll(GetShapeInfoInputDto input);

        Task<ShapeInfoDto> DataPost(CreateOrUpdateShapeInfoDto input);

        Task Delete(List<Guid> ids);

        Task<ShapeInfoDto> GetLatestByNameAndLine(GetShapeInfoInputDto input);
        
        Task<List<ShapeInfoDto>> BatchUpdateCauseAsync(BatchUpdateShapeInfoDto input);
    }
}
