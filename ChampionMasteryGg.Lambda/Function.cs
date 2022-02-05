using Amazon.Lambda.APIGatewayEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ChampionMasteryGg
{
    public class Function : APIGatewayFunctionBase
    {
        private static ChampionMasteryGgClient cmggClient;

        static Function() => Initialize();

        static void Initialize()
        {
            AWSSDKHandler.RegisterXRayForAllServices();
            cmggClient = new();
        }

        public override async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest invocationEvent, ILambdaContext context)
        {
            //return Found(invocationEvent); // For debugging
            switch(invocationEvent.PathParameters.Single().Value)
            {
                case "highscores":
                    return Found(await cmggClient.GetHighscoresAsync());
                case "champion":
                    switch (invocationEvent.QueryStringParameters.Single().Value)
                    {
                        case "-1":
                            return Found(await cmggClient.GetTotalPointsHighscoresAsync());
                        case "-2":
                            return Found(await cmggClient.GetTotalLevelHighscoresAsync());
                        default:
                            var championId = int.Parse(invocationEvent.QueryStringParameters.Single().Value);
                            return Found(await cmggClient.GetHighscoresAsync(championId));
                    }
                default:
                    return NotFound();
            }
        }
    }
}
