using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Sitecore.Infrastructure.Factories;
using Moq;

namespace Provider.Sitecore.Test.SitecoreProvider
{
  public abstract class SitecoreProviderTest
  {
    protected readonly ProviderBase Sut;

    protected Mock<ISitecoreClientFactory> NameClientFactory;
    protected Mock<IWindsorContainer> Container;

    protected SitecoreProviderTest()
    {
      Container = new Mock<IWindsorContainer>();
      NameClientFactory = new Mock<ISitecoreClientFactory>();
      var applicationContext = new ApplicationContext(Container.Object);
      Sut = new CluedIn.Provider.Sitecore.SitecoreProvider(applicationContext, NameClientFactory.Object);
    }
  }
}
