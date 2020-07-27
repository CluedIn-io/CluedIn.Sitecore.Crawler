using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Sitecore.Vocabularies
{
    public class InteractionVocabulary : SimpleVocabulary
    {
        public InteractionVocabulary()
        {
            VocabularyName = "Sitecore Interaction";
            KeyPrefix = "sitecore.interaction";
            KeySeparator = ".";
            Grouping = EntityType.Activity;

            AddGroup("Sitecore Details", group =>
            {
                Id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                StartDateTime = group.Add(new VocabularyKey(nameof(StartDateTime), VocabularyKeyDataType.DateTime, VocabularyKeyVisibility.Visible));
                Duration = group.Add(new VocabularyKey(nameof(Duration), VocabularyKeyDataType.Duration, VocabularyKeyVisibility.Visible));
                UserAgent = group.Add(new VocabularyKey(nameof(UserAgent), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                EngagementValue = group.Add(new VocabularyKey(nameof(EngagementValue), VocabularyKeyDataType.Number, VocabularyKeyVisibility.Visible));
            });

        }

        public VocabularyKey Id { get; private set; }
        public VocabularyKey StartDateTime { get; private set; }
        public VocabularyKey Duration { get; private set; }
        public VocabularyKey UserAgent { get; private set; }
        public VocabularyKey EngagementValue { get; private set; }

    }
}
