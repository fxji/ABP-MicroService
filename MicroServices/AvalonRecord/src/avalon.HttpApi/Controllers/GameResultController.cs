using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace avalon.controllers
{

    public class GameResultController : avalonController, IGameResultAppService
    {
        
        private readonly IGameResultAppService _gameResultAppService;


        public GameResultController(IGameResultAppService gameResultAppService)
        {
            _gameResultAppService = gameResultAppService;
        }

        public Task<GameResultDto> CreateAsync(CreateOrUpdateGameResultDto input)
        {
            return _gameResultAppService.CreateAsync(input);
        }

        public Task<PagedResultDto<GameResultDto>> GetListAsync(GetGameResultInputDto input)
        {
            return _gameResultAppService.GetListAsync(input);
        }
    }
}