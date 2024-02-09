using LinkShortener.API.Controllers;
using LinkShortener.API.Controllers.Models;
using LinkShortener.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class LinksControllerTests
{
    private readonly LinksController _controller;
    private readonly Mock<ILinkShortenerService> _mockService;

    public LinksControllerTests()
    {
        _mockService = new Mock<ILinkShortenerService>();
        _controller = new LinksController(_mockService.Object);
    }

    [Fact]
    public async Task CreateShortLink_Returns_CreatedLink_WhenUrlIsValid()
    {
        // Arrange
        var request = new CreateLinkRequest { OriginalUrl = "https://example.com" };
        var expectedResponse = new CreateLinkResponse { ShortUrl = "abc123", FullUrl = "https://yourdomain.com/linkshortener/abc123" };
        _mockService.Setup(s => s.GenerateShortLink(It.IsAny<string>())).ReturnsAsync("abc123");

        // Act
        var result = await _controller.CreateShortLink(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CreateLinkResponse>(okResult.Value);
        Assert.Equal(expectedResponse.ShortUrl, response.ShortUrl);
        Assert.Equal(expectedResponse.FullUrl, response.FullUrl);
    }
}
