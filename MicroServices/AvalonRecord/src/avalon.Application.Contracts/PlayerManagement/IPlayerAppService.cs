using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

public interface IPlayerAppService : IApplicationService
{
    Task<PagedResultDto<PlayerDto>> GetListAsync(GetPlayerInputDto input);
    Task<PlayerDto> CreateAsync(CreateOrUpdatePlayerDto input);
    Task<PlayerDto> UpdateAsync( CreateOrUpdatePlayerDto input);
    Task DeleteAsync(Guid id);
}