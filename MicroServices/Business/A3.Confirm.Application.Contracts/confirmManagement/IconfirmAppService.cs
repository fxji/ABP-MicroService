using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using A3.Confirm.ConfirmManagement.Dto;

namespace A3.Confirm.ConfirmManagement
{
    public interface IConfirmAppService : IApplicationService
    {
        Task<ConfirmDto> Get(Guid id);

        Task<PagedResultDto<ConfirmDto>> GetAll(GetConfirmInputDto input);

        Task<ConfirmDto> DataPost(CreateOrUpdateConfirmDto input);

        Task Delete(List<Guid> ids);
    }
}
