using Sitecore.Xdb.Common.Web;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace CluedIn.Crawling.Sitecore.Infrastructure.Sitecore
{
    public class CertificateWebHandlerModifier : IWebRequestHandlerModifier
    {
        private readonly X509Certificate _x509Certificate;

        public CertificateWebHandlerModifier(string rawdata)
        {
            _x509Certificate = new X509Certificate(Convert.FromBase64String(rawdata));
        }

        public void Process(HttpClientHandler handler)
        {

            if (handler is WebRequestHandler webRequestHandler)
            {
                if (_x509Certificate == null)
                {
                    //TODO: manage exception 
                    throw new Exception();
                }
                webRequestHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                webRequestHandler.ClientCertificates.Add(_x509Certificate);
            }
        }
    }
}
