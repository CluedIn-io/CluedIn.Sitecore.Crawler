using CluedIn.Crawling.Sitecore.Infrastructure.Installers;
using Xunit;

namespace Crawling.Sitecore.Test
{
  public class AutoMapperTests
  {
    [Fact]
    public void ConfigurationIsValid()
    {
      MapperConfiguration.GetMapperConfiguration().AssertConfigurationIsValid();
    }
  }
}
