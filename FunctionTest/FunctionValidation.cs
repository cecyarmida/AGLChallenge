﻿using FluentAssertions;
using Functions;
using Microsoft.Azure.WebJobs.Extensions;
using System;
using System.Net.Http;
//using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FunctionTest
{
    [TestClass]
    public class FunctionValidation
    {
        [TestMethod]
        public async Task ValidateContent_Test()
        {
            //Mocks AGL's Json to return a valid Json
            Environment.SetEnvironmentVariable("JsonURL", "https://aglmapjason.azurewebsites.net/mock/agl-json");
            // Arrange
            var req = new HttpRequestMessage()
            {
                Content = new StringContent(string.Empty),
                RequestUri = new Uri($"https://aglmapjason.azurewebsites.net/mock/agl-json")
            };
            var log = new TraceMonitor();

            // Act
            var result = await JsonMapper.Run(req, log).ConfigureAwait(false);

            // Assert
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            content.Should().NotContainEquivalentOf("Dog");

        }

        [TestMethod]
        public async Task ValidateError_Test()
        {
            //Mocks AGL's Json to return a invalid Json
            Environment.SetEnvironmentVariable("JsonURL", "https://aglmapjason.azurewebsites.net/mock/agl-json-error");
            // Arrange
            var req = new HttpRequestMessage()
            {
                Content = new StringContent(string.Empty),
                RequestUri = new Uri($"https://aglmapjason.azurewebsites.net/mock/agl-json-error")
            };
            var log = new TraceMonitor();

            // Act
            var result = await JsonMapper.Run(req, log).ConfigureAwait(false);

            // Assert
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            content.Should().ContainEquivalentOf("Error");

        }
    }
}
