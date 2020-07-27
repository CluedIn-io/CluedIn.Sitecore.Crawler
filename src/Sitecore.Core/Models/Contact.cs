using System;
using System.Collections.Generic;

namespace CluedIn.Crawling.Sitecore.Core.Models
{
    public class Contact
    {
        public PersonalInfo Personal { get; set; }
        public IEnumerable<Interaction> Interactions { get; set; }
        public IEnumerable<ContactIdentifier> Identifiers { get; set; }
        public Guid? Id { get; set; }
        public EmailAddressList Emails { get; set; }
    }
}
