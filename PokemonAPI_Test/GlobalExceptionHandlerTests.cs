using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PokedexAPI.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonAPI_Test
{
    public class GlobalExceptionHandlerTests
    {
        [Fact]
        public async Task TryHandleAsync_ShouldLogErrorAndReturnProblemDetails_WhenExceptionIsThrown()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GlobalExceptionHandler>>();
            var exceptionHandler = new GlobalExceptionHandler(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var exception = new Exception("Test exception");

            // Simulate a request to create a valid context
            httpContext.Request.Method = "GET";
            httpContext.Request.Path = "/api/test";
            httpContext.Request.QueryString = new QueryString("?param=value");

            // Capture the response
            using var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;

            // Act
            var result = await exceptionHandler.TryHandleAsync(httpContext, exception, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);

            // Read the response body
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseContent = await new StreamReader(responseBody).ReadToEndAsync();

            // Deserialize the response to check its contents
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent);

            Assert.NotNull(problemDetails);
            Assert.Equal("Test exception param=value", problemDetails.Detail);
            Assert.Equal("HttpMethod: GET Path/api/test", problemDetails.Instance);
            Assert.Equal((int)HttpStatusCode.InternalServerError, problemDetails.Status);
            Assert.Equal("API Server Error", problemDetails.Title);
            Assert.Equal("Server Error", problemDetails.Type);
        }
    }
}