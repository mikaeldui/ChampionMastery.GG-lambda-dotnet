using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.Model;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using System.Threading.Tasks;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

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
            try
            {
                if (invocationEvent.RawPath == "/")
                {
                    return Found(await cmggClient.GetHighscoresAsync());
                }
                else if (invocationEvent.RawPath == "/level")
                {
                    return Found(await cmggClient.GetTotalLevelHighscoresAsync());
                }
                else if (invocationEvent.RawPath == "/points")
                {
                    return Found(await cmggClient.GetTotalPointsHighscoresAsync());
                }
                else if (invocationEvent.RawPath.StartsWith("/champions"))
                {
                    var championId = int.Parse(invocationEvent.RawPath["/champions/".Length..]);
                    return Found(await cmggClient.GetHighscoresAsync(championId));
                }
                else
                    return NotFound();
            }
            catch
            {
                return Error();
            }
        }
    }
}
