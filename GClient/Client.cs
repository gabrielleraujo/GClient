using GClient.Engine;
using Microsoft.Extensions.Logging;

namespace GClient
{
    public static class Client
    {
        public static async Task<TGet> GetAsync<TGet>(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        where TGet : class => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync<TGet>(HttpMethod.Get, route, data, headers, completionOption, cancellationToken);

        public static async Task<string> GetAsync(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
            => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync(HttpMethod.Get, route, data, headers, completionOption, cancellationToken);


        public static async Task<TPost> PostAsync<TPost>(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        where TPost : class => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync<TPost>(HttpMethod.Post, route, data, headers, completionOption, cancellationToken);

        public static async Task<string> PostAsync(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
            => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync(HttpMethod.Post, route, data, headers, completionOption, cancellationToken);


        public static async Task<TPut> PutAsync<TPut>(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        where TPut : class => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync<TPut>(HttpMethod.Put, route, data, headers, completionOption, cancellationToken);

        public static async Task<string> PutAsync(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
            => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync(HttpMethod.Put, route, data, headers, completionOption, cancellationToken);


        public static async Task<TDelete> DeleteAsync<TDelete>(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        where TDelete : class => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync<TDelete>(HttpMethod.Delete, route, data, headers, completionOption, cancellationToken);

        public static async Task<string> DeleteAsync(
            this string route,
            object? data = null, Dictionary<string, string>? headers = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
            => await new ClientEngine(new LoggerFactory().CreateLogger<ClientEngine>()).SendRequestAsync(HttpMethod.Delete, route, data, headers, completionOption, cancellationToken);
    }
}