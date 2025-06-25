using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Threading;

public interface IGameResultAppService : IApplicationService
{
    Task<PagedResultDto<GameResultDto>> GetListAsync(GetGameResultInputDto input);

    Task<GameResultDto> CreateAsync(CreateOrUpdateGameResultDto input);
}