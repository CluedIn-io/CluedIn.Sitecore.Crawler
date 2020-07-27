using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Sitecore.Core;
using CluedIn.Crawling.Sitecore.Infrastructure.Factories;

namespace CluedIn.Crawling.Sitecore
{
    public class SitecoreCrawler : ICrawlerDataGenerator
    {
        private readonly ISitecoreClientFactory _clientFactory;
        public SitecoreCrawler(ISitecoreClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is SitecoreCrawlJobData sitecorecrawlJobData))
            {
                yield break;
            }

            var client = _clientFactory.CreateNew(sitecorecrawlJobData);

            using (var cursor = client.CreateContactCursor(jobData.LastCrawlFinishTime.UtcDateTime))
            {
                while (cursor.MoveNext())
                {
                    foreach (var contact in cursor.Current)
                    {
                        yield return contact;
                        foreach (var interaction in contact.Interactions)
                        {
                            interaction.Personal = contact.Personal;
                            yield return interaction;
                        }
                    }
                }
            }
        }
    }
}
