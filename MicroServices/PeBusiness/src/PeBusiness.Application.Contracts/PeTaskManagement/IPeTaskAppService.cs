using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using PeBusiness.PeTaskManagement.Dto;

namespace PeBusiness.PeTaskManagement
{
    public interface IPeTaskAppService : IApplicationService
    {
        Task<PeTaskDto> Get(Guid id);

        Task<PagedResultDto<PeTaskDto>> GetAll(GetPeTaskInputDto input);

        Task<PeTaskDto> DataPost(CreateOrUpdatePeTaskDto input);

        Task Delete(List<Guid> ids);
    }
}
