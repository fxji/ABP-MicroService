using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.A3AttachmentManagement.Dto;

namespace AAA.A3AttachmentManagement
{
    public interface IA3AttachmentAppService : IApplicationService
    {
        Task<A3AttachmentDto> Get(Guid id);

        Task<PagedResultDto<A3AttachmentDto>> GetAll(GetA3AttachmentInputDto input);

        Task<A3AttachmentDto> DataPost(CreateOrUpdateA3AttachmentDto input);

        Task Delete(List<Guid> ids);
    }
}
