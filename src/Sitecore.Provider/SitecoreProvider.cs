using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Configuration;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.Sitecore.Core;
using CluedIn.Crawling.Sitecore.Infrastructure.Factories;
using CluedIn.Providers.Models;

namespace CluedIn.Provider.Sitecore
{
    public class SitecoreProvider : ProviderBase
    {
        private readonly ISitecoreClientFactory _sitecoreClientFactory;

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        public SitecoreProvider([NotNull] ApplicationContext appContext, ISitecoreClientFactory sitecoreClientFactory)
            : base(appContext, SitecoreConstants.CreateProviderMetadata())
        {
            _sitecoreClientFactory = sitecoreClientFactory;
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        public override async Task<CrawlJobData> GetCrawlJobData(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var sitecoreCrawlJobData = new SitecoreCrawlJobData();
            if (configuration.ContainsKey(SitecoreConstants.KeyName.CertData))
            { sitecoreCrawlJobData.CertData = configuration[SitecoreConstants.KeyName.CertData].ToString(); }
            if (configuration.ContainsKey(SitecoreConstants.KeyName.Uri))
            { sitecoreCrawlJobData.Uri = configuration[SitecoreConstants.KeyName.Uri].ToString(); }

            return await Task.FromResult(sitecoreCrawlJobData);
        }

        public override Task<bool> TestAuthentication(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            var dictionary = new Dictionary<string, object>();

            if (jobData is SitecoreCrawlJobData sitecoreCrawlJobData)
            {
                dictionary.Add(SitecoreConstants.KeyName.CertData, sitecoreCrawlJobData.CertData);
                dictionary.Add(SitecoreConstants.KeyName.Uri, sitecoreCrawlJobData.Uri);
            }

            return await Task.FromResult(dictionary);
        }

        public override Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId,
            string folderId)
        {

            throw new NotImplementedException();
        }

        public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            var sitecoreCrawlJobData = jobData as SitecoreCrawlJobData;

            if (sitecoreCrawlJobData == null)
            {
                throw new Exception("Wrong CrawlJobData type");
            }

            var client = _sitecoreClientFactory.CreateNew(sitecoreCrawlJobData);
            return await Task.FromResult(client.GetAccountInformation());
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            return webHooksEnabled && ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
                : $"{relativeDateTime.Minute} 0/4 * * *";
        }

        public override Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            throw new NotImplementedException();
        }

        public override Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));

            throw new NotImplementedException();
        }

        public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));



            throw new NotImplementedException();
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));


            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }
    }
}
