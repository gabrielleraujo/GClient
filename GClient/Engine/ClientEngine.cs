using System.Diagnostics;
using Flurl.Http;
using GClient.Factories;
using Microsoft.Extensions.Logging;

namespace GClient.Engine
{
    public class ClientEngine
    {
        public readonly ILogger<ClientEngine> _logger;
        public ClientEngine(ILogger<ClientEngine> logger) => _logger = logger;

        public async Task<TDeserialize> SendRequestAsync<TDeserialize>(
            HttpMethod verb, string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
            where TDeserialize : class
        {
            var result = await SendRequestAsync(verb, route, data, headers, completionOption, cancellationToken);
            return result.DeserializeObject<TDeserialize>();
        }

        public async Task<string> SendRequestAsync(
            HttpMethod verb, string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var watch = Stopwatch.StartNew();

            if (data == null) { data = new object { }; }
            if (headers == null) { headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } }; }

            var response = await route.WithHeaders(headers).SendJsonAsync(verb, data, cancellationToken, completionOption);

            var responseContent = await response.GetStringAsync();

            _logger.LogInformation("Response StatusCode {statusCode}.", response.StatusCode);
            _logger.LogInformation("Response Content {responseContent}.", responseContent);
            _logger.LogInformation("Request Url: {route} Integration Service Request Time: {elapsedMilliseconds}.", route, watch.ElapsedMilliseconds);
            watch.Stop();

            return responseContent;
        }
    }
}