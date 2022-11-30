using Flurl.Http;
using Flurl.Http.Testing;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using GClient.Test.Extensions;
using GClient.Engine;
using GClient.Test.Factories;

namespace GClient.Test.UnitTest
{
    public class ClientEngineTest
    {
        public Mock<ILogger<ClientEngine>> _logger { get; set; }
        private ClientEngine _clientBase;
        private readonly string _route;

        public ClientEngineTest()
        {
            _logger = new Mock<ILogger<ClientEngine>>();
            _clientBase = new ClientEngine(_logger.Object);
            _route = "https://test.com/user";
        }

        [Trait("ServerestIntegration", "SendRequestAsync")]
        [Fact(DisplayName = "SendRequestAsync_WhenResponseSuccess_NotThowsException - Success")]
        public async Task SendRequestAsync_WhenResponseSuccess_NotThowsException()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new { Message = "Success" }, 200);

            var response = await _clientBase.SendRequestAsync(HttpMethod.Post, _route, new { Name = "Someone"});

            Assert.Contains("Success", response);
            LogLevel.Information.Verify(Times.Exactly(3), _logger);
        }

        [Trait("ServerestIntegration", "SendRequestAsyncGeneric")]
        [Fact(DisplayName = "SendRequestAsyncGeneric_WhenResponseSuccess_NotThowsException - Success")]
        public async Task SendRequestAsyncGeneric_WhenResponseSuccess_NotThowsException()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new { Message = "Success" }, 200);

            var response = await _clientBase.SendRequestAsync<Response>(HttpMethod.Post, _route, new { Name = "Someone" });

            Assert.Equal("Success", response.Message);
            LogLevel.Information.Verify(Times.Exactly(3), _logger);
        }

        [Trait("ServerestIntegration", "SendRequestAsyncGeneric")]
        [Theory(DisplayName = "SendRequestAsyncGeneric_WhenResponseError_ThowsFlurlHttpException - Error")]
        [InlineData(400)]
        [InlineData(500)]
        public async Task SendRequestAsyncGeneric_WhenResponseError_ThowsIntegrationException(int status)
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWith("server error", status);
            await _clientBase.Invoking(x => x.SendRequestAsync<Response>(HttpMethod.Post, _route, new { Message = "Success" })).Should().ThrowAsync<FlurlHttpException>();
        }

        [Trait("ServerestIntegration", "SendRequestAsyncGeneric")]
        [Fact(DisplayName = "SendRequestAsyncGeneric_WhenResponseSuccessButObjSerializedIsIncorrect_ThowsJsonReaderException - Error")]
        public async Task SendRequestAsyncGeneric_WhenResponseSuccessButObjSerializedIsIncorrect_ThowsJsonReaderException()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWith("response!", 200);
            await _clientBase.Invoking(x => x.SendRequestAsync<Response>(HttpMethod.Post, _route, new { Message = "Success" })).Should().ThrowAsync<JsonReaderException>();
        }
    }
}
