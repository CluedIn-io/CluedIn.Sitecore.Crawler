using System;
using System.Collections.Generic;
using CluedIn.Core.Net.Mail;
using CluedIn.Core.Providers;

namespace CluedIn.Crawling.Sitecore.Core
{
    public class SitecoreConstants
    {
        public const string AnonymousContactDisplayName = "Anonymous Sitecore Contact";

        public struct KeyName
        {
            public static readonly string CertData = nameof(CertData);
            public static readonly string Uri = nameof(Uri);
        }

        public const string CodeOrigin = "Sitecore";
        public const string ProviderRootCodeValue = "Sitecore";
        public const string CrawlerName = "SitecoreCrawler";
        public const string CrawlerComponentName = "SitecoreCrawler";
        public const string s_crawlerDescription = "Sitecore is a .NET CMS with a rich set of marketing features. Integrate all the Sitecore xProfile contact information into CluedIn.";

        public const string CrawlerDisplayName = "Sitecore";
        public const string Uri = "http://www.sampleurl.com";



        public static readonly Guid ProviderId = Guid.Parse("6516d9e6-88b6-49f3-b0d9-dc8ba445aa98");
        public const string ProviderName = "Sitecore";
        public const bool SupportsConfiguration = true;             // TODO: Replace value
        public const bool SupportsWebHooks = false;
        public const bool SupportsAutomaticWebhookCreation = false;
        public const bool RequiresAppInstall = false;
        public const string AppInstallUrl = null;
        public const string ReAuthEndpoint = null;
        public const string IconUri = "https://s3-eu-west-1.amazonaws.com/staticcluedin/bitbucket.png"; // TODO: Replace value

        public static readonly ComponentEmailDetails ComponentEmailDetails = new ComponentEmailDetails
        {
            Features = new Dictionary<string, string>
            {
                                       { "Tracking",        "Expenses and Invoices against customers" },
                                       { "Intelligence",    "Aggregate types of invoices and expenses against customers and companies." }
                                   },
            Icon = new Uri(IconUri),
            ProviderName = ProviderName,
            ProviderId = ProviderId,
            Webhooks = SupportsWebHooks
        };

        public static IProviderMetadata CreateProviderMetadata()
        {
            return new ProviderMetadata
            {
                Id = ProviderId,
                ComponentName = CrawlerName,
                Name = ProviderName,
                Type = "Cloud",
                SupportsConfiguration = SupportsConfiguration,
                SupportsWebHooks = SupportsWebHooks,
                SupportsAutomaticWebhookCreation = SupportsAutomaticWebhookCreation,
                RequiresAppInstall = RequiresAppInstall,
                AppInstallUrl = AppInstallUrl,
                ReAuthEndpoint = ReAuthEndpoint,
                ComponentEmailDetails = ComponentEmailDetails
            };
        }
    }
}
