using System;
using Xunit;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Crawling.Sitecore.ClueProducers;
using CluedIn.Crawling.Sitecore.Vocabularies;
using CluedIn.Crawling.Sitecore.Core.Models;
using CluedIn.Crawling;

namespace Crawling.Sitecore.Test.ClueProducers
{
    public class InteractionClueProducerTests : ClueProducerTest<Interaction>
    {
        protected override BaseClueProducer<Interaction> Sut { get; }

        protected override EntityType ExpectedEntityType => EntityType.Activity;

        public InteractionClueProducerTests()
        {
            Sut = new InteractionClueProducer(
                _clueFactory.Object,
                new InteractionVocabulary(),
                _logger.Object);
        }

        [Theory,AutoData]
        public void ClueHasEdgeToContact(Interaction interaction)
        {
            var clue = Sut.MakeClue(interaction, Guid.NewGuid());
            _clueFactory.Verify(
                f => f.CreateOutgoingEntityReference(
                    clue, EntityType.Infrastructure.Contact,
                    EntityEdgeType.RequestedBy,
                    interaction.ContactId.ToString(),
                    interaction.ContactId.ToString())
                );
        }

        [Theory]
        [InlineAutoData("StartDateTime")]
        [InlineAutoData("Duration")]
        [InlineAutoData("Id")]
        [InlineAutoData("UserAgent")]
        [InlineAutoData("EngagementValue")]
        public void ClueHasPropertiesSet(string propertyName, Interaction interaction)
        {
            AssertPropertyKeyIsPopulatedWithKeyFrom<InteractionVocabulary>
                (Sut.MakeClue(interaction, Guid.NewGuid()), propertyName);
        }
    }
}
