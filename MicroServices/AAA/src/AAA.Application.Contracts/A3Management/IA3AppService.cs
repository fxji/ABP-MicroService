using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AAA.A3Management.Dto;

namespace AAA.A3Management
{
    public interface IA3AppService : IApplicationService
    {
        Task<A3Dto> Get(Guid id);

        Task<string> Export(Guid id);


        Task<PagedResultDto<A3Dto>> GetAll(GetA3InputDto input);

        Task<A3Dto> DataPost(CreateOrUpdateA3Dto input);

        Task Delete(List<Guid> ids);

        Task Share(ShareDto shareInfo);
    }
}
