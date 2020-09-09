using CluedIn.Crawling.Sitecore.Core;

namespace CluedIn.Crawling.Sitecore
{
    public class SitecoreCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<SitecoreCrawlJobData>
    {
        public SitecoreCrawlerJobProcessor(SitecoreCrawlerComponent component) : base(component)
        {
        }
    }
}
