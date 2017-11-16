using FluentAssertions;
using Functions;
using Microsoft.Azure.WebJobs.Extensions;
using System;
using System.Net.Http;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionUnitTest
{
   // [TestClass]
    public class UnitTest1
    {
        [Fact]
        public async void ValidateContent_Test()
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
            content.Should().ContainEquivalentOf("Cat");

        }
    }
}
