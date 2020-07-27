using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Sitecore.Core;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.Sitecore.Factories
{
  public class SitecoreClueFactory : ClueFactory
  {
    public SitecoreClueFactory()
        : base(SitecoreConstants.CodeOrigin, SitecoreConstants.ProviderRootCodeValue)
    {
    }

    protected override Clue ConfigureProviderRoot([NotNull] Clue clue)
    {
      if (clue == null) throw new ArgumentNullException(nameof(clue));

      var data = clue.Data.EntityData;
      data.Name = SitecoreConstants.CrawlerName;
      data.Uri = new Uri(SitecoreConstants.Uri);
      data.Description = SitecoreConstants.s_crawlerDescription;


      clue.ValidationRuleSuppressions.AddRange(new[]
          {
            RuleConstants.METADATA_002_Uri_MustBeSet
          });

      return clue;
    }
  }
}
