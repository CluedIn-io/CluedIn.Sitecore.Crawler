using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;
using Vocabs = CluedIn.Core.Data.Vocabularies.Vocabularies;

namespace CluedIn.Crawling.Sitecore.Vocabularies
{
    public class ContactVocabulary : SimpleVocabulary
    {
        public ContactVocabulary()
        {
            VocabularyName = "Sitecore Contact";
            KeyPrefix = "sitecore.contact";
            KeySeparator = ".";
            Grouping = EntityType.Infrastructure.Contact;


            AddGroup("Sitecore Details", group =>
            {
                Id = group.Add(new VocabularyKey(nameof(Id), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                FirstName = group.Add(new VocabularyKey(nameof(FirstName), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                MiddleName = group.Add(new VocabularyKey(nameof(MiddleName), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                LastName = group.Add(new VocabularyKey(nameof(LastName), VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                Nickname = group.Add(new VocabularyKey(nameof(Nickname), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Suffix = group.Add(new VocabularyKey(nameof(Suffix), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Title = group.Add(new VocabularyKey(nameof(Title), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Gender = group.Add(new VocabularyKey(nameof(Gender), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                JobTitle = group.Add(new VocabularyKey(nameof(JobTitle), VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                PreferredEmail = group.Add(new VocabularyKey(nameof(PreferredEmail), VocabularyKeyDataType.Email, VocabularyKeyVisibility.Visible));
                OtherEmails = group.Add(new VocabularyKey(nameof(OtherEmails), VocabularyKeyDataType.Email, VocabularyKeyVisibility.Visible));
                Twitter = group.Add(new VocabularyKey(nameof(Twitter), VocabularyKeyDataType.Email, VocabularyKeyVisibility.Visible));

            });

            // Mappings to Common Vocabulary
            // TODO should it be CluedInUser or CluedInPerson?
            AddMapping(FirstName, Vocabs.CluedInUser.FirstName);
            AddMapping(LastName, Vocabs.CluedInUser.LastName);
            AddMapping(MiddleName, Vocabs.CluedInUser.MiddleName);
            AddMapping(Gender, Vocabs.CluedInUser.Gender);
            AddMapping(JobTitle, Vocabs.CluedInUser.JobTitle);
            AddMapping(Title, Vocabs.CluedInUser.Title);
            AddMapping(PreferredEmail, Vocabs.CluedInUser.Email);
            AddMapping(OtherEmails, Vocabs.CluedInUser.EmailAddresses);
            AddMapping(Twitter, Vocabs.CluedInUser.SocialTwitter);
        }

        public VocabularyKey Id { get; private set; }
        public VocabularyKey FirstName { get; private set; }
        public VocabularyKey MiddleName { get; private set; }
        public VocabularyKey LastName { get; private set; }
        public VocabularyKey Nickname { get; private set; }
        public VocabularyKey Suffix { get; private set; }
        public VocabularyKey Title { get; private set; }
        public VocabularyKey Gender { get; private set; }
        public VocabularyKey JobTitle { get; private set; }
        public VocabularyKey PreferredEmail { get; private set; }
        public VocabularyKey OtherEmails { get; private set; }
        public VocabularyKey Twitter { get; private set; }
    }
}
