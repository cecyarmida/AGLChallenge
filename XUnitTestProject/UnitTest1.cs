using Functions;
using System;
using System.Net.Http;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest
    {
        [Theory]
        [InlineData("Azure")]
        [InlineData("Dependency")]
        public async void Given_Dependency_HttpTrigger_ShouldReturn_Result()
        {
            // Arrange
            var req = new HttpRequestMessage()
            {
                Content = new StringContent(string.Empty),
                RequestUri = new Uri($"http://localhost")
            };
            var log = new TraceMonitor();

            // Act
            var result = await JsonMapper.Run(req, log).ConfigureAwait(false);

            // Assert
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.DoesNotContain("Dog,", content);
        }
    }
}
