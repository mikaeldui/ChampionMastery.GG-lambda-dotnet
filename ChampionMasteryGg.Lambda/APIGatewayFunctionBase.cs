using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
