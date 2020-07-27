using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Core;
using CluedIn.Crawling.Sitecore.Vocabularies;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;
using CluedIn.Crawling.Sitecore.Core.Models;
using CluedIn.Core.Logging;
using CluedIn.Crawling.Helpers;
using System.Linq;
using CluedIn.Crawling.Sitecore.Core;

namespace CluedIn.Crawling.Sitecore.ClueProducers
{
    public class ContactClueProducer : BaseClueProducer<Contact>
    {
        private readonly IClueFactory _factory;
        private readonly ContactVocabulary _vocabulary;
        private readonly ILogger _logger;

        public ContactClueProducer(IClueFactory factory, ContactVocabulary vocabulary, ILogger logger)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _vocabulary = vocabulary ?? throw new ArgumentNullException(nameof(vocabulary));
            _logger = logger;
        }

        protected override Clue MakeClueImpl(Contact input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(_vocabulary.Grouping, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.DisplayName = input.Personal.FullNameDisplayName;

            var fullName = input.Personal?.FullName;
            data.Name = string.IsNullOrWhiteSpace(fullName) ? input.Id.ToString() : fullName;

            data.Properties[_vocabulary.Id] = input.Id.PrintIfAvailable();
            data.Properties[_vocabulary.FirstName] = input.Personal.FirstName.PrintIfAvailable();
            data.Properties[_vocabulary.LastName] = input.Personal.LastName.PrintIfAvailable();
            data.Properties[_vocabulary.MiddleName] = input.Personal.MiddleName.PrintIfAvailable();
            data.Properties[_vocabulary.Suffix] = input.Personal.Suffix.PrintIfAvailable();
            data.Properties[_vocabulary.Title] = input.Personal.Suffix.PrintIfAvailable();
            data.Properties[_vocabulary.Gender] = input.Personal.Gender.PrintIfAvailable();
            data.Properties[_vocabulary.JobTitle] = input.Personal.JobTitle.PrintIfAvailable();
            data.Properties[_vocabulary.Nickname] = input.Personal.Nickname.PrintIfAvailable();
            data.Properties[_vocabulary.PreferredEmail] = input.Emails?.PreferredEmail?.StmpAddress.PrintIfAvailable();

            input?.Emails?.Others?.Select(email => email.StmpAddress).ForEach(
               email => data.Properties.Add(_vocabulary.OtherEmails, email)
                );

            data.Properties[_vocabulary.Twitter] = input?.Identifiers.FirstOrDefault(id => id.Source == "twitter")?.Identifier;

            clue.ValidationRuleSuppressions.AddRange(new[]
                {
                    RuleConstants.METADATA_002_Uri_MustBeSet,
                    RuleConstants.METADATA_003_Author_Name_MustBeSet,
                });

            _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            _logger.Info($"Sending clue {data.Name}");
            return clue;
        }
    }
}
