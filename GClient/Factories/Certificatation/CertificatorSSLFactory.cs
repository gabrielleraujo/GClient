using Flurl.Http.Configuration;
using System.Net.Security;

namespace GClient.Factories.Certification
{
    public class CertificatorSSLFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) =>
                {
                    if (errors == SslPolicyErrors.None)
                    {
                        return true;
                    }

                    return false;
                }
            };
        }
    }
}
