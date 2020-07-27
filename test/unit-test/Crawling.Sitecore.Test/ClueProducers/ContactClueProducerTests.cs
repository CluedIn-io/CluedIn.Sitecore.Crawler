using System;
using System.Collections.Generic;
using Xunit;
using AutoFixture.Xunit2;
using Should;
using CluedIn.Core.Data;
using CluedIn.Crawling.Sitecore.ClueProducers;
using CluedIn.Crawling.Sitecore.Vocabularies;
using CluedIn.Crawling.Sitecore.Core.Models;
using CluedIn.Crawling;
using CluedIn.Crawling.Sitecore.Core;

namespace Crawling.Sitecore.Test.ClueProducers
{
    public class ContactClueProducerTests : ClueProducerTest<Contact>
    {
        protected override BaseClueProducer<Contact> Sut { get; }

        protected override EntityType ExpectedEntityType => EntityType.Infrastructure.Contact;

        public ContactClueProducerTests()
        {
            Sut = new ContactClueProducer(
                _clueFactory.Object,
                new ContactVocabulary(),
                _logger.Object);
        }

        [Theory, AutoData]
        public void ClueHasEdgeToProvider(Contact contact)
        {
            var clue = Sut.MakeClue(contact, Guid.NewGuid());
            _clueFactory.Verify(
                f => f.CreateEntityRootReference(clue, EntityEdgeType.PartOf)
                );
        }

        [Theory]
        [InlineAutoData("Id")]
        [InlineAutoData("FirstName")]
        [InlineAutoData("MiddleName")]
        [InlineAutoData("LastName")]
        [InlineAutoData("Nickname")]
        [InlineAutoData("Suffix")]
        [InlineAutoData("Title")]
        [InlineAutoData("Gender")]
        [InlineAutoData("JobTitle")]
        [InlineAutoData("PreferredEmail")]
        [InlineAutoData("OtherEmails")]
        public void ClueWillHavePopulatedProperties(string propertyName, Contact contact) =>
            AssertPropertyKeyIsPopulatedWithKeyFrom<ContactVocabulary>(
                Sut.MakeClue(contact, Guid.NewGuid()),
                propertyName);

        [Theory]
        [InlineAutoData("Twitter","twitter")]
        public void ClueHasTwitterIdentifier(string vocabKey, string identifierSource, Contact contact, ContactIdentifier identifier)
        {
            var identifiers = new List<ContactIdentifier>(contact.Identifiers);
            identifier.Source = identifierSource;
            identifiers.Add(identifier);
            contact.Identifiers = identifiers;

            AssertPropertyKeyIsPopulatedWithKeyFrom<ContactVocabulary>(
                Sut.MakeClue(contact,Guid.NewGuid()),vocabKey
                );
        }

        [Theory]
        [InlineAutoData("John","Doe","John Doe")]
        [InlineAutoData("", "Doe", "Doe")]
        [InlineAutoData("John", "", "John")]
        public void ClueNameIsformedFromFirstNameLastName(string firstName, string lastName, string expected, Contact contact)
        {
            contact.Personal.FirstName = firstName;
            contact.Personal.LastName = lastName;

            Sut.MakeClue(contact, Guid.NewGuid()).Data.EntityData.Name.ShouldEqual(expected);
        }

        [Theory,AutoData]
        public void ClueNameDefaultsToId(Contact contact)
        {
            contact.Personal.FirstName = null;
            contact.Personal.LastName = null;

            Sut.MakeClue(contact, Guid.NewGuid()).Data.EntityData.Name.ShouldEqual(contact.Id.ToString());
        }

        [Theory,AutoData]
        public void DisplayNameDefaultsToMessage(Contact contact)
        {
            var expected = SitecoreConstants.AnonymousContactDisplayName;

            contact.Personal.FirstName = null;
            contact.Personal.LastName = null;

            Sut.MakeClue(contact, Guid.NewGuid()).Data.EntityData.DisplayName.ShouldEqual(expected);
        }
    }
}
