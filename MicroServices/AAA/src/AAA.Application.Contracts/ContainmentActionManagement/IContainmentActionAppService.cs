using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.ContainmentActionManagement.Dto;

namespace AAA.ContainmentActionManagement
{
    public interface IContainmentActionAppService : IApplicationService
    {
        Task<ContainmentActionDto> Get(Guid id);

        Task<PagedResultDto<ContainmentActionDto>> GetAll(GetContainmentActionInputDto input);

        Task<ContainmentActionDto> DataPost(CreateOrUpdateContainmentActionDto input);

        Task Delete(List<Guid> ids);
    }
}
