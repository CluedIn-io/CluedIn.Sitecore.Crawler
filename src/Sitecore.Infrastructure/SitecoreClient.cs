using System;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Sitecore.Core;
using System.Collections.Generic;
using Sitecore.XConnect;
using CluedIn.Crawling.Sitecore.Infrastructure.Sitecore;
using Sitecore.Xdb.Common.Web;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Schema;
using Sitecore.XConnect.Collection.Model;
using System.Threading.Tasks;
using Nito.AsyncEx;
using AutoMapper;

namespace CluedIn.Crawling.Sitecore.Infrastructure
{
    public class SitecoreClient
    {
        private readonly ILogger _log;
        private readonly IMapper _mapper;
        private readonly SitecoreCrawlJobData _jobData;

        public SitecoreClient(ILogger log, SitecoreCrawlJobData sitecoreCrawlJobData, IMapper mapper)
        {
            _jobData = sitecoreCrawlJobData ?? throw new ArgumentNullException(nameof(sitecoreCrawlJobData));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _mapper = mapper;
        }

        private async Task<XConnectClientConfiguration> InitializeConnectionAsync()
        {
            var certificateModifier = new CertificateWebHandlerModifier(_jobData.CertData);

            var clientModifiers = new List<IHttpClientModifier>();
            var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
            clientModifiers.Add(timeoutClientModifier);

            // This overload takes three client end points - collection, search, and configuration
            var collectionClient = new CollectionWebApiClient(new Uri($"https://{_jobData.Uri}/odata"), clientModifiers, new[] { certificateModifier });
            var searchClient = new SearchWebApiClient(new Uri($"https://{_jobData.Uri}/odata"), clientModifiers, new[] { certificateModifier });
            var configurationClient = new ConfigurationWebApiClient(new Uri($"https://{_jobData.Uri}/configuration"), clientModifiers, new[] { certificateModifier });

            var cfg = new XConnectClientConfiguration(
              new XdbRuntimeModel(CollectionModel.Model), collectionClient, searchClient, configurationClient, true);

            try
            {
                await cfg.InitializeAsync().ConfigureAwait(false);
                return cfg;
            }
            catch (XdbModelConflictException ce)
            {
                _log.Error(() => ce.Message, ce);
                return null;
            }
        }

        public ContactCursor CreateContactCursor(DateTime startTime)
        {
            _log.Debug("Entering Get Contacts");

            var cfg = AsyncContext.Run(async () => await InitializeConnectionAsync());

            return new ContactCursor(new XConnectClient(cfg), startTime, _mapper);
        }

        public AccountInformation GetAccountInformation()
        {
            var name = $"Sitecore.XConnect ({_jobData.Uri})";

            return new AccountInformation(name, name);
        }
    }
}
