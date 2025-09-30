using JouteMythologique.Core;
using Xunit;

public class BattleRulesTests
{
    [Theory]
    [InlineData(God.Zeus, God.Ares, 1)]
    [InlineData(God.Ares, God.Zeus, -1)]
    [InlineData(God.Zeus, God.Zeus, 0)]
    [InlineData(God.Hades, God.Artemis, 1)]
    [InlineData(God.Artemis, God.Hades, -1)]
    public void TestFightRules(God g1, God g2, int expected)
    {
        Assert.Equal(expected, BattleRules.Fight(g1, g2));
    }
}