using Volo.Abp.DependencyInjection;

public class DefaultAvalonScoreCalculator : IScoreCalculator, ITransientDependency
{
    public int CalculateScore(GamePlayerDto player, GameOutcome gameOutcome, int playerCount)
    {
        // BlueWins， Resistance 每人得分 baseScore*3， Spy-baseScore，player.MistakeKilled 额外得分 baseScore
        // RedWinsByAssassination ， Spy 每人得分 baseScore*3， Resistance-baseScore，Role assassin 额外得分 baseScore
        // RedWinsByAll， Spy 每人得分 baseScore*3， Resistance-baseScore
        int baseScore = playerCount;

        switch (gameOutcome)
        {
            case GameOutcome.BlueWins:
                return player.Role.GetAffiliation() == Affiliation.Resistance
                    ? baseScore * 3 + (player.MistakeKilled ? baseScore : 0)
                    : -baseScore;

            case GameOutcome.RedWinsByAssassination:
                return player.Role.GetAffiliation() == Affiliation.Spy
                    ? baseScore * 2 + (player.Role == Role.Assassin ? baseScore : 0)
                    : -baseScore;

            case GameOutcome.RedWinsByAll:
                return player.Role.GetAffiliation() == Affiliation.Spy
                    ? baseScore * 3
                    : -baseScore;

            default:
                return baseScore;
        }
        
    }
}