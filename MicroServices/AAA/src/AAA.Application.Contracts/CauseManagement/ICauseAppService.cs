using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.CauseManagement.Dto;

namespace AAA.CauseManagement
{
    public interface ICauseAppService : IApplicationService
    {
        Task<CauseDto> Get(Guid id);

        Task<PagedResultDto<CauseDto>> GetAll(GetCauseInputDto input);

        Task<CauseDto> DataPost(CreateOrUpdateCauseDto input);

        Task Delete(List<Guid> ids);
    }
}
