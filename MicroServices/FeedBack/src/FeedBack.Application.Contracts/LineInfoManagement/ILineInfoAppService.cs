using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Feedback.LineInfoManagement.Dto;

namespace Feedback.LineInfoManagement
{
    public interface ILineInfoAppService : IApplicationService
    {
        Task<LineInfoDto> Get(Guid id);

        Task<PagedResultDto<LineInfoDto>> GetAll(GetLineInfoInputDto input);

        Task<LineInfoDto> DataPost(CreateOrUpdateLineInfoDto input);

        Task Delete(List<Guid> ids);
    }
}
