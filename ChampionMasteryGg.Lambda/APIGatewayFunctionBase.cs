using Amazon.Lambda.APIGatewayEvents;

namespace Amazon.Lambda
{
    public abstract class APIGatewayFunctionBase
    {
        public abstract Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest invocationEvent, ILambdaContext context);

        public static APIGatewayHttpApiV2ProxyResponse Found(object @object) => new() { Body = JsonSerializer.Serialize(@object), StatusCode = 200 };

        public static APIGatewayHttpApiV2ProxyResponse NotFound() => new() { StatusCode = 404 };

        public static APIGatewayHttpApiV2ProxyResponse Error() => new() { StatusCode = 500 };
    }
}
