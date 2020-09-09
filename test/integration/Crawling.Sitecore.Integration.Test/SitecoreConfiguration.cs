using System.Collections.Generic;
using CluedIn.Crawling.Sitecore.Core;

namespace CluedIn.Crawling.Sitecore.Integration.Test
{
  public static class SitecoreConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { SitecoreConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
