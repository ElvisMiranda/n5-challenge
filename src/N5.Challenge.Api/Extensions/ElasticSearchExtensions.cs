using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Api.Extensions;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["ElasticSettings:BaseUrl"];
        var apiKey = configuration["ElasticSettings:ApiKey"];

        var authentication = new ApiKey(apiKey);
        var clientSettings = new ElasticsearchClientSettings(new Uri(baseUrl))
            .Authentication(authentication)
            .DefaultMappingFor<Permission>(i => i.IndexName("permissions").IdProperty(p => p.Id))
            .EnableDebugMode()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));
        
        var client = new ElasticsearchClient(clientSettings);
        
        services.AddSingleton(client);
    }
}