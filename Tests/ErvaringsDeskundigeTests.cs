using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

using WDPR_i_API;
using Xunit;

namespace Tests;
using Xunit;

public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _client;

    public EndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory;
    }

//     protected override IHost CreateHost(IHostBuilder builder)
//  {
//      builder.UseContentRoot(Directory.GetCurrentDirectory());
//      return base.CreateHost(builder);
//  }


    [Theory]
    [InlineData("api/ErvaringsDeskundige")]
    // [InlineData("/Onderzoek")]
    // [InlineData("/bedrijvenportaal")]
    // [InlineData("/Privacy")]
    // [InlineData("/Contact")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _client
            .WithWebHostBuilder(builder => builder.UseContentRoot("C:\\Users\\Chris\\OneDrive - De Haagse Hogeschool\\2023 - 2024 WPR\\WDPR\\WDPR-i-API\\"))
            .CreateClient();


        // Act
        var response = await client.GetAsync(url);
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetErvaringsDeskundigeFail404NotFound()
    {
        // Arrange
        var client = _client
            .WithWebHostBuilder(builder => builder.UseContentRoot("C:\\Users\\Chris\\OneDrive - De Haagse Hogeschool\\2023 - 2024 WPR\\WDPR\\WDPR-i-API\\"))
            .CreateClient();
        var url = "api/ErvaringsDeskundige/5";
        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }

    [Fact]
    public async Task GetErvaringsDeskundigeSuccess200()
    {
        // Arrange
        var client = _client
            .WithWebHostBuilder(builder => builder.UseContentRoot("C:\\Users\\Chris\\OneDrive - De Haagse Hogeschool\\2023 - 2024 WPR\\WDPR\\WDPR-i-API\\"))
            .CreateClient();
        var url = "api/ErvaringsDeskundige/153e5526-bbfc-43d0-ab57-324e292f4428";
        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
}
