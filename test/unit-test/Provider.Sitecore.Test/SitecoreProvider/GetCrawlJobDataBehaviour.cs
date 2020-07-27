using AutoFixture.Xunit2;
using CluedIn.Core.Providers;
using Should;
using System;
using System.Collections.Generic;

using Xunit;

namespace Provider.Sitecore.Test.SitecoreProvider
{
  public class GetCrawlJobDataBehaviour : SitecoreProviderTest
  {
    [Theory]
    [InlineAutoData]
    public void GetCrawlJobDataTests(Dictionary<string, object> dictionary, Guid organizationId, Guid userId, Guid providerDefinitionId)
    {

      Sut.GetCrawlJobData(null, dictionary, organizationId, userId, providerDefinitionId)
          .ShouldNotBeNull();
    }
  }
}
