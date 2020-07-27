using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.Sitecore;
using CluedIn.Crawling.Sitecore.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Sitecore.Test
{
  public class SitecoreCrawlerBehaviour
  {
    private readonly ICrawlerDataGenerator _sut;

    public SitecoreCrawlerBehaviour()
    {
        var nameClientFactory = new Mock<ISitecoreClientFactory>();

        _sut = new SitecoreCrawler(nameClientFactory.Object);
    }

    [Fact]
    public void GetDataReturnsData()
    {
      var jobData = new CrawlJobData();

      _sut.GetData(jobData)
          .ShouldNotBeNull();
    }
  }
}
