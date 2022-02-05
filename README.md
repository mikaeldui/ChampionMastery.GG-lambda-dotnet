# ChampionMastery.GG .NET Lambda (unofficial)
An **unofficial** Lambda for transforming the [ChampionMastery.GG](https://github.com/Derpthemeus/ChampionMastery.GG) high scores into JSON.

It's using the [`MikaelDui.ChampionMasteryGg.Client`](https://github.com/mikaeldui/ChampionMastery.GG-dotnet-client) library.

# Prerequisites 

    dotnet tool install -g Amazon.Lambda.Tools
    
# Packaging

    dotnet lambda package

# Consuming the Lambda
With the [`MikaelDui.ChampionMasteryGg.Client`](https://github.com/mikaeldui/ChampionMastery.GG-dotnet-client) library.

    ChampionMasteryGgClient.BaseAddress = "https://1a2b3c4d5e.execute-api.eu-west-2.amazonaws.com/";
    ChampionMasteryGgClient.Encoding = ChampionMasteryGgEncoding.Json;
    using ChampionMasteryGgClient client = new();
    var highscores = await client.GetHighscoresAsync();
