using CluedIn.Crawling.Sitecore.Core;

namespace CluedIn.Crawling.Sitecore.Infrastructure.Factories
{
    public interface ISitecoreClientFactory
    {
        SitecoreClient CreateNew(SitecoreCrawlJobData sitecoreCrawlJobData);
    }
}
