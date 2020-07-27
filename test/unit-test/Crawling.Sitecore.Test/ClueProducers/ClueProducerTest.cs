using System;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;
using CluedIn.Core.Logging;
using CluedIn.Crawling;
using CluedIn.Crawling.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Sitecore.Test.ClueProducers
{
    public abstract class ClueProducerTest<T>
    {
        protected readonly Mock<ILogger> _logger;
        protected readonly Mock<IClueFactory> _clueFactory;

        protected abstract BaseClueProducer<T> Sut { get; }
        protected abstract EntityType ExpectedEntityType { get; }

        public ClueProducerTest()
        {
            _logger = new Mock<ILogger>();
            _clueFactory = new Mock<IClueFactory>();
            var entityCode = new Mock<IEntityCode>();

            _clueFactory.Setup(f =>
                f.Create(It.IsAny<EntityType>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(new Clue(entityCode.Object, Guid.NewGuid()));
        }

        [Theory, AutoData]
        public void ClueHasId(T input) =>
            Sut.MakeClue(input, Guid.NewGuid())
                .Id.ShouldNotBeSameAs(Guid.Empty);

        [Theory, AutoData]
        public void ClueHasName(T input) =>
            Sut.MakeClue(input, Guid.NewGuid())
                .Data.EntityData.Name.ShouldNotBeEmpty();

        [Theory, AutoData]
        public void ClueHasDisplayName(T input) =>
           Sut.MakeClue(input, Guid.NewGuid())
            .Data.EntityData.DisplayName.ShouldNotBeEmpty();

        [Theory, AutoData]
        protected void ClueIsOfType(T input)
        {
            Sut.MakeClue(input, Guid.NewGuid());
            _clueFactory.Verify(
                f => f.Create(ExpectedEntityType, It.IsAny<string>(), It.IsAny<Guid>())
                );
        }

        protected void AssertPropertyKeyIsPopulatedWithKeyFrom<S>(Clue clue, string propertyName) where S : SimpleVocabulary, new()
        {
            var vocabulary = new S();
            var key = (VocabularyKey)vocabulary.GetType().GetProperty(propertyName).GetValue(vocabulary);
            clue.Data.EntityData.Properties[key].ShouldNotBeNull();
        }
    }
}
