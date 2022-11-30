using Flurl.Http.Configuration;

namespace GClient.Factories.Certification
{
    public class ByPassCertificatorSSLFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) => true
            };
        }
    }
}
