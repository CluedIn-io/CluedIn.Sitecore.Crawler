using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Core;
using CluedIn.Crawling.Sitecore.Vocabularies;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;
using CluedIn.Crawling.Sitecore.Core.Models;
using CluedIn.Core.Logging;
using CluedIn.Crawling.Helpers;

namespace CluedIn.Crawling.Sitecore.ClueProducers
{
    public class InteractionClueProducer : BaseClueProducer<Interaction>
    {
        private readonly IClueFactory _factory;
        private readonly InteractionVocabulary _vocabulary;
        private readonly ILogger _logger;

        public InteractionClueProducer(IClueFactory factory, InteractionVocabulary vocabulary, CluedIn.Core.Logging.ILogger logger)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _vocabulary = vocabulary ?? throw new ArgumentNullException(nameof(vocabulary));
            _logger = logger;
        }

        protected override Clue MakeClueImpl(Interaction input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(_vocabulary.Grouping, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.Name = data.DisplayName = input.ToString();

            data.Properties[_vocabulary.StartDateTime] = input.StartDateTime.ToShortDateString();
            data.Properties[_vocabulary.Duration] = input.Duration.TotalMinutes.ToString();
            data.Properties[_vocabulary.Id] = input.Id.ToString();
            data.Properties[_vocabulary.UserAgent] = input.UserAgent.PrintIfAvailable();
            data.Properties[_vocabulary.EngagementValue] = input.EngagementValue.PrintIfAvailable();

            clue.ValidationRuleSuppressions.AddRange(new[]
                {
                RuleConstants.METADATA_002_Uri_MustBeSet,
                RuleConstants.METADATA_003_Author_Name_MustBeSet,
                RuleConstants.METADATA_005_PreviewImage_RawData_MustBeSet
                });

            _factory.CreateOutgoingEntityReference(clue,
              EntityType.Infrastructure.Contact,
              EntityEdgeType.RequestedBy, input.ContactId.ToString(), input.ContactId.ToString());

            return clue;
        }
    }
}
