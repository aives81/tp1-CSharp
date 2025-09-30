namespace JouteMythologique.Core;

public static class BattleRules
{
    private static readonly Dictionary<God, God[]> WinsAgainst = new()
    {
        { God.Zeus, new[] { God.Ares, God.Poseidon } },
        { God.Athena, new[] { God.Zeus, God.Ares } },
        { God.Hades, new[] { God.Zeus, God.Artemis } },
        { God.Ares, new[] { God.Hades, God.Artemis } },
        { God.Poseidon, new[] { God.Athena, God.Hades } },
        { God.Artemis, new[] { God.Poseidon, God.Athena } }
    };

    public static int Fight(God g1, God g2)
    {
        if (g1 == g2) return 0; // égalité
        if (WinsAgainst[g1].Contains(g2)) return 1; // g1 gagne
        if (WinsAgainst[g2].Contains(g1)) return -1; // g2 gagne
        return 0; // sécurité
    }
}