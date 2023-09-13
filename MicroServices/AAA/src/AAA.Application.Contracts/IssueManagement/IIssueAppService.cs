using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.IssueManagement.Dto;

namespace AAA.IssueManagement
{
    public interface IIssueAppService : IApplicationService
    {
        Task<IssueDto> Get(Guid id);

        Task<PagedResultDto<IssueDto>> GetAll(GetIssueInputDto input);

        Task<IssueDto> DataPost(CreateOrUpdateIssueDto input);

        Task Delete(List<Guid> ids);
    }
}
