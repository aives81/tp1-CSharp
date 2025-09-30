using System.Net.Http.Json;
using JouteMythologique.Core;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Téléchargement des résultats...");

        using var client = new HttpClient();
        var url = "https://raw.githubusercontent.com/JLou/dotnet-course/refs/heads/main/e1.json";

        var rounds = await client.GetFromJsonAsync<List<MatchRound>>(url);

        if (rounds == null || rounds.Count == 0)
        {
            Console.WriteLine("Aucun résultat trouvé !");
            return;
        }

        var score = new Dictionary<string, int>();
        var godUsage = new Dictionary<God, int>();

        foreach (var round in rounds)
        {
            if (!score.ContainsKey(round.Player1)) score[round.Player1] = 0;
            if (!score.ContainsKey(round.Player2)) score[round.Player2] = 0;

            if (!godUsage.ContainsKey(round.God1)) godUsage[round.God1] = 0;
            if (!godUsage.ContainsKey(round.God2)) godUsage[round.God2] = 0;
            godUsage[round.God1]++;
            godUsage[round.God2]++;

            int result = BattleRules.Fight(round.God1, round.God2);

            if (result == 1) score[round.Player1]++;
            else if (result == -1) score[round.Player2]++;
        }

        // Vainqueur
        var winner = score.OrderByDescending(s => s.Value).First();
        Console.WriteLine($"\n🏆 Le vainqueur est {winner.Key} avec {winner.Value} victoires !");

        // Stats avancées
        Console.WriteLine("\n📊 Statistiques :");
        Console.WriteLine("Dieux les plus utilisés :");
        foreach (var kvp in godUsage.OrderByDescending(g => g.Value))
        {
            Console.WriteLine($" - {kvp.Key}: {kvp.Value} utilisations");
        }

        Console.WriteLine("\nTaux de victoire par joueur :");
        int totalRounds = rounds.Count;
        foreach (var kvp in score)
        {
            Console.WriteLine($" - {kvp.Key}: {(kvp.Value * 100.0 / totalRounds):F2}% de victoires");
        }
    }
}
