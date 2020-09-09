using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Sitecore.Core;
using CluedIn.Crawling.Sitecore.Infrastructure.Factories;

namespace CluedIn.Crawling.Sitecore
{
    public class SitecoreCrawler : ICrawlerDataGenerator
    {
        private readonly ISitecoreClientFactory clientFactory;
        public SitecoreCrawler(ISitecoreClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is SitecoreCrawlJobData sitecorecrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(sitecorecrawlJobData);

            //retrieve data from provider and yield objects
            
        }       
    }
}
