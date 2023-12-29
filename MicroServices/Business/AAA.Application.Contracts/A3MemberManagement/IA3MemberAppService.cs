using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.A3MemberManagement.Dto;

namespace AAA.A3MemberManagement
{
    public interface IA3MemberAppService : IApplicationService
    {
        Task<A3MemberDto> Get(Guid id);

        Task<PagedResultDto<A3MemberDto>> GetAll(GetA3MemberInputDto input);

        Task<A3MemberDto> DataPost(CreateOrUpdateA3MemberDto input);

        Task Delete(List<Guid> ids);
    }
}
