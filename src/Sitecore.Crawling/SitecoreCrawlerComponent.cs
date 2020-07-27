using CluedIn.Core;
using CluedIn.Crawling.Sitecore.Core;

using ComponentHost;

namespace CluedIn.Crawling.Sitecore
{
    [Component(SitecoreConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class SitecoreCrawlerComponent : CrawlerComponentBase
    {
        public SitecoreCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

