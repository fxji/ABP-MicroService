using System;

public enum GameOutcome
{
    BlueWins,
    RedWinsByAssassination,
    RedWinsByAll,
    Draw
}


// Role 是有红蓝阵营的enum
public enum Role
{
    Merlin,
    Assassin,
    Percival,
    Morgana,
    Mordred,
    LoyalServant,
    MinionOfMordred,
    Oberon
}

public enum Affiliation
{
    Resistance, // 蓝方
    Spy         // 红方
}


public static class RoleExtensions
{
    public static Affiliation GetAffiliation(this Role role)
    {
        return role switch
        {
            Role.Merlin => Affiliation.Resistance,
            Role.Percival => Affiliation.Resistance,
            Role.LoyalServant => Affiliation.Resistance,

            Role.Assassin => Affiliation.Spy,
            Role.Morgana => Affiliation.Spy,
            Role.Mordred => Affiliation.Spy,
            Role.MinionOfMordred => Affiliation.Spy,
            Role.Oberon => Affiliation.Spy,

            _ => throw new ArgumentOutOfRangeException(nameof(role), $"Unknown role: {role}")
        };
    }
}