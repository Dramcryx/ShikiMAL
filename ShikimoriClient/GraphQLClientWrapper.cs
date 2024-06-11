using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace ShikimoriClient;

/// <summary>
/// Обёртка над GraphQL, создающая запросы, и разбрающая ответы
/// </summary>
public class GraphQLClientWrapper
{
    private readonly Token token;

    private readonly GraphQLHttpClient client;

    protected GraphQLClientWrapper(string endpoint, Token token)
    {
        this.token = token;
        client = new GraphQLHttpClient(endpoint, new NewtonsoftJsonSerializer());
        client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
    }

    protected async Task<T> GetAsync<T>(string query)
    {
        var response = await client.SendQueryAsync<T>(new GraphQLRequest
        {
            Query = query
        });

        if (response.Errors != null)
        {
            throw new HttpRequestException(string.Join('\n', response.Errors.Select(x => x.Message)));
        }

        return response.Data;
    }
}
