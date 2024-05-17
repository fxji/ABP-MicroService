using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.ConfirmInfoManagement.Dto;

namespace AAA.ConfirmInfoManagement
{
    public interface IConfirmInfoAppService : IApplicationService
    {
        Task<ConfirmInfoDto> Get(Guid id);

        Task<PagedResultDto<ConfirmInfoDto>> GetAll(GetConfirmInfoInputDto input);

        Task<ConfirmInfoDto> DataPost(CreateOrUpdateConfirmInfoDto input);

        Task Delete(List<Guid> ids);
    }
}
