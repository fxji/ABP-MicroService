using AutoMapper;

public class GameResultAutoMapperProfile : Profile
{
    public GameResultAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<GameResult, GameResultDto>();
        CreateMap<CreateOrUpdateGameResultDto, GameResult>();
    }
}