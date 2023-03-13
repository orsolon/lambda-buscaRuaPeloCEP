using Amazon.Lambda.Core;
using System.Net.Http.Json;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CEP_AWSLambda;

public class Function
{
    private static readonly HttpClient client = new HttpClient();
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandlerAsync(string input, ILambdaContext context)
    {
        if(input != null)
        {
            var streamTask = await client.GetStreamAsync($"https://viacep.com.br/ws/{input}/json/");
            var endereco = await JsonSerializer.DeserializeAsync<Endereco>(streamTask);
            return endereco.logradouro;
        }
        return "CEP não informado";
    }
}
