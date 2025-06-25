using AutoMapper;

public class PlayerAutoMapperProfile : Profile
{
    public PlayerAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Player, PlayerDto>();
        CreateMap<CreateOrUpdatePlayerDto, Player>();


        CreateMap<GamePlayer, GamePlayerDto>();
    }
}