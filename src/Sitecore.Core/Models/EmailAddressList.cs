using System.Collections.Generic;

namespace CluedIn.Crawling.Sitecore.Core.Models
{
    public class EmailAddressList
    {
        public EmailAddress PreferredEmail { get; set; }
        public IEnumerable<EmailAddress> Others { get; set; }
    }
}
