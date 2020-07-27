using System;

namespace CluedIn.Crawling.Sitecore.Core.Models
{
    public class Interaction
    {
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string UserAgent { get; set; }
        public Guid? Id { get; set; }
        public Guid ContactId { get; set; }
        public PersonalInfo Personal { get; set; }
        public int EngagementValue { get; set; }

        public override string ToString()
        {
            return $"Website visit by {Personal.FullNameDisplayName} at {StartDateTime}";
        }
    }
}
