namespace CluedIn.Crawling.Sitecore.Core.Models
{
  public class ContactIdentifier
  {
    public enum ContactIdentifierType { Anonymous, Known }

    public string Identifier { get; set; }
    public string Source { get; set; }
    public ContactIdentifierType IdentifierType { get; set; }
  }
}
