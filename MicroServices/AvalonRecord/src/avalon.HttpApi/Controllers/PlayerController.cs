using System;
using System.Threading.Tasks;
using avalon;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace avalon.controllers
{
    [Area(avalonRemoteServiceConsts.ModuleName)]
    [Route("api/avalon/player")]
    public class PlayerController : avalonController, IPlayerAppService
    {
        protected IPlayerAppService PlayerAppService { get; }
        public PlayerController(IPlayerAppService playerAppService)
        {
            PlayerAppService = playerAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<PlayerDto>> GetListAsync([FromQuery] GetPlayerInputDto input)
        {
            return await PlayerAppService.GetListAsync(input);
        }

        
        
        [HttpPut]
        [Route("{id}")] 
        public async Task<PlayerDto> UpdateAsync(Guid id, CreateOrUpdatePlayerDto input)
        {
            return await PlayerAppService.UpdateAsync(input);
        }

        [HttpPost]
        public Task<PlayerDto> CreateAsync(CreateOrUpdatePlayerDto input)
        {
           return PlayerAppService.CreateAsync(input);
        }

        [HttpPut]
        public Task<PlayerDto> UpdateAsync(CreateOrUpdatePlayerDto input)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}