using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.CorrectiveActionManagement.Dto;

namespace AAA.CorrectiveActionManagement
{
    public interface ICorrectiveActionAppService : IApplicationService
    {
        Task<CorrectiveActionDto> Get(Guid id);

        Task<PagedResultDto<CorrectiveActionDto>> GetAll(GetCorrectiveActionInputDto input);

        Task<CorrectiveActionDto> DataPost(CreateOrUpdateCorrectiveActionDto input);

        Task Delete(List<Guid> ids);
    }
}
