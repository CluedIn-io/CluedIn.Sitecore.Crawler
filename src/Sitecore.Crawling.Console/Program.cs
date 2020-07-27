using CluedIn.Core.Data;
using CluedIn.Crawling;
using CluedIn.Crawling.Sitecore.Core;
using System.IO;
using System.Reflection;

namespace CluedIn.CrawlerSitecore.Console
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
      var p = new DebugCrawlerHost<SitecoreCrawlJobData>(executingFolder, SitecoreConstants.ProviderName);

      p.ProcessClue += MethodDoingSomethingWithClue;

      p.Execute(SitecoreConfiguration.Create(), SitecoreConstants.ProviderId);
    }

    private static void MethodDoingSomethingWithClue(Clue clue)
    {
      // This is your opportunity to add custom actions for clue processing testing
      //var info = clue.Serialize(); // this outputs the full data of the clue. Useful for debugging.
      var info = clue.OriginEntityCode.Value; //Just outputs the ID of the clue
      System.Console.WriteLine(info);
    }
  }
}
