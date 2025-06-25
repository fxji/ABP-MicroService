using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using FeedBack.ProgramInfoManagement.Dto;

namespace FeedBack.ProgramInfoManagement
{
    public interface IProgramInfoAppService : IApplicationService
    {
        Task<ProgramInfoDto> Get(Guid id);

        Task<PagedResultDto<ProgramInfoDto>> GetAll(GetProgramInfoInputDto input);

        Task<ProgramInfoDto> DataPost(CreateOrUpdateProgramInfoDto input);

        Task Delete(List<Guid> ids);

        Task<ProgramInfoDto> GetLatestByNameAndLine(GetProgramInfoInputDto input);
    }
}
