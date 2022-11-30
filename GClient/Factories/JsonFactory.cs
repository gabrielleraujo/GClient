using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GClient.Factories
{
    [ExcludeFromCodeCoverage]
    public static class JsonFactory
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static string SerializeObject<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }

        public static T DeserializeObject<T>(this string json, string dateFormat)
        {
            return JsonConvert.DeserializeObject<T>(json, new IsoDateTimeConverter { DateTimeFormat = dateFormat });
        }
    }
}