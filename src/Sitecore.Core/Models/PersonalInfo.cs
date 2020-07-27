using System;

namespace CluedIn.Crawling.Sitecore.Core.Models
{
    public class PersonalInfo
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string JobTitle { get; set; }

        public override string ToString()
        {
            return FullNameDisplayName;
        }

        public string FullName =>
            $"{FirstName} {LastName}".Trim();

        public string FullNameDisplayName =>
            string.IsNullOrWhiteSpace(FullName) ? SitecoreConstants.AnonymousContactDisplayName : FullName;

  }
}
