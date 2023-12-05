using System.Net;
using System.Text;
using System.Text.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

namespace N5.Challenge.IntegrationTests;

public class PermissionsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public PermissionsIntegrationTests(TestingWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Returns_Success()
    {
        // Arrange
        const int clientId = 1;
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/permissions");

        var input = new Dictionary<string, string>
        {
            { "forename", "User" },
            { "surname", "Test"},
            { "permissionTypeId", "1" }
        };

        postRequest.Content = GetSerializedInput(input);

        // Act 
        var response = await _client.SendAsync(postRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        response.Headers.Should().ContainKey("Location");
        responseString.Should().Contain($"{clientId}");
    }

    [Fact]
    public async Task Post_Returns_PermissionTypeNotFound()
    {
        // Arrange
        const int clientId = 1;
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/permissions");

        var input = new Dictionary<string, string>
        {
            { "forename", "User" },
            { "surname", "Test"},
            { "permissionTypeId", "3" }
        };

        postRequest.Content = GetSerializedInput(input);

        // Act 
        var response = await _client.SendAsync(postRequest);

        // Assert
        
        var responseString = await response.Content.ReadAsStringAsync();
        
        responseString.Should().Contain("Permission Type was not found");
    }

    [Fact]
    public async Task Put_Returns_Success()
    {
        // Arrange
        const int clientId = 1;
        var putRequest = new HttpRequestMessage(HttpMethod.Put, $"api/permissions/{clientId}");
        var input = new Dictionary<string, string>
        {
            { "forename", "User" },
            { "surname", "Test"},
            { "permissionTypeId", "1" }
        };

        putRequest.Content = GetSerializedInput(input);

        // Act
        var response = await _client.SendAsync(putRequest);

        // Assert 
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Get_Returns_Permission()
    {
        // Arrange
        const int clientId = 1;
        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"api/permissions/{clientId}");

        // Act
        var response = await _client.GetAsync(getRequest.RequestUri);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Contain("User");
        responseString.Should().Contain("Test");
    }

    private static StringContent GetSerializedInput(Dictionary<string, string> input)
     => new(JsonSerializer.Serialize(input), Encoding.Unicode, "application/json");

}