public interface IScoreCalculator
{
    int CalculateScore(GamePlayerDto role, GameOutcome outcome,int playerCount);
}