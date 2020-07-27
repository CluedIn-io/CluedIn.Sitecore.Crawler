using System;
using System.Linq;
using CluedIn.Core.Crawling;
using AutoFixture.Xunit2;
using Should;
using Xunit;
using CluedIn.Crawling.Sitecore.Core;

namespace Provider.Sitecore.Test.SitecoreProvider
{
    public class GetHelperConfigurationBehaviour : SitecoreProviderTest
    {
        private readonly CrawlJobData _jobData;

        public GetHelperConfigurationBehaviour()
        {
            _jobData = new SitecoreCrawlJobData();
        }

        [Fact]
        public void Throws_ArgumentNullException_With_Null_CrawlJobData_Parameter()
        {
            var ex = Assert.Throws<AggregateException>(
                () => Sut.GetHelperConfiguration(null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
                    .Wait());

            ((ArgumentNullException)ex.InnerExceptions.Single())
                .ParamName
                .ShouldEqual("jobData");
        }

        [Theory]
        [InlineAutoData]
        public void Returns_ValidDictionary_Instance(Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            Sut.GetHelperConfiguration(null, _jobData, organizationId, userId, providerDefinitionId)
                .Result
                .ShouldNotBeNull();
        }




        [Theory]
        [InlineAutoData("CertData", "CertData")]
        [InlineAutoData("Uri", "Uri")]
        // Fill in the values for expected results ....
        public void Returns_Expected_Data(string key, string propertyName, object expectedValue, Guid organizationId, Guid userId, Guid providerDefinitionId) // TODO add additional parameters to populate CrawlJobData instance
        {
            var property = _jobData.GetType().GetProperty(propertyName);
            property?.SetValue(_jobData, expectedValue);

            var result = Sut.GetHelperConfiguration(null, _jobData, organizationId, userId, providerDefinitionId)
                            .Result;

            result
                .ContainsKey(key)
                .ShouldBeTrue(
                    $"{key} not found in results");

            result[key]
                .ShouldEqual(expectedValue);
        }
    }
}
