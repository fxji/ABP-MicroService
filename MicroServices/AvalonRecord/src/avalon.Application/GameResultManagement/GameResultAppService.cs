using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

public class GameResultAppService : ApplicationService, IGameResultAppService
{
    private readonly IRepository<GameResult, Guid> _gameResultRepository;
    private readonly IRepository<Player, Guid> _playerRepo;
    private readonly IScoreCalculator _scoreCalculator;

    public GameResultAppService(IRepository<GameResult, Guid> gameResultRepository, IRepository<Player, Guid> playerRepo, IScoreCalculator scoreCalculator)
    {
        _gameResultRepository = gameResultRepository;
        _playerRepo = playerRepo;
        _scoreCalculator = scoreCalculator;
    }

    public async Task<GameResultDto> CreateAsync(CreateOrUpdateGameResultDto input)
    {
        GameResult result = new GameResult(GuidGenerator.Create(), DateTime.Now, input.Outcome);

        foreach (var item in input.GamePlayers)
        {
            result.AddPlayer(GuidGenerator.Create(), item.PlayerId, item.Role, item.MistakeKilled);

            var player = await _playerRepo.GetAsync(item.PlayerId);

            int score = _scoreCalculator.CalculateScore(item, input.Outcome, input.GamePlayers.Count());

            player.AddScore(score);
        }

        var data = await _gameResultRepository.InsertAsync(result);

        return ObjectMapper.Map<GameResult, GameResultDto>(data);
    }

    public async Task<GameResultDto> GetAsync(Guid id)
    {
        var result = await _gameResultRepository.GetAsync(id);
        return ObjectMapper.Map<GameResult, GameResultDto>(result);
    }

    public async Task<PagedResultDto<GameResultDto>> GetListAsync(GetGameResultInputDto input)
    {

        // 获取gameResult的同时，获取gamePlayers
        var query = await _gameResultRepository.WithDetailsAsync(gr => gr.GamePlayers);

        var result = query.OrderByDescending(gr => gr.PlayedAt).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var dto = ObjectMapper.Map<List<GameResult>, List<GameResultDto>>(result);

        return new PagedResultDto<GameResultDto>
        {
            TotalCount = await _gameResultRepository.GetCountAsync(),
            Items = dto
        };
    }


}