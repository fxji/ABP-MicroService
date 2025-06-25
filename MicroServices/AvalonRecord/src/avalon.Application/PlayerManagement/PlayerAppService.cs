using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

public class PlayerAppService : ApplicationService, IPlayerAppService
{
    private readonly IRepository<Player, Guid> _playerRepository;
    // private readonly IPlayerManager _playerManager;

    // public PlayerAppService(IRepository<Player, Guid> playerRepository, IPlayerManager playerManager)
    // {
    //     _playerRepository = playerRepository;
    //     _playerManager = playerManager;
    // }

    public PlayerAppService(IRepository<Player, Guid> playerRepository)
    {
        _playerRepository = playerRepository;
    }


    public async Task<PlayerDto> GetAsync(Guid id)
    {
        var result = await _playerRepository.GetAsync(id);
        return ObjectMapper.Map<Player, PlayerDto>(result);
    }

    public async Task<PlayerDto> CreateAsync(CreateOrUpdatePlayerDto input)
    {
        var result = await _playerRepository.InsertAsync(ObjectMapper.Map<CreateOrUpdatePlayerDto, Player>(input));
        return ObjectMapper.Map<Player, PlayerDto>(result);
    }

    public async Task<PlayerDto> UpdateAsync(CreateOrUpdatePlayerDto input)
    {
        var result = await _playerRepository.UpdateAsync(ObjectMapper.Map<CreateOrUpdatePlayerDto, Player>(input));
        return ObjectMapper.Map<Player, PlayerDto>(result);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _playerRepository.DeleteAsync(id);
    }

    public async Task<PagedResultDto<PlayerDto>> GetListAsync(GetPlayerInputDto input)
    {
        var query = await _playerRepository.GetQueryableAsync();

        var totalCount = await AsyncExecuter.CountAsync(query);

        var items = query.OrderBy(input.Sorting ?? "Id desc")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

        var dto = ObjectMapper.Map<List<Player>, List<PlayerDto>>(items);

        return new PagedResultDto<PlayerDto>(totalCount, dto);
    }
}