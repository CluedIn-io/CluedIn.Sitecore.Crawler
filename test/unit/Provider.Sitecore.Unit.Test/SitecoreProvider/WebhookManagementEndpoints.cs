using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Xunit;

namespace CluedIn.Provider.Sitecore.Unit.Test.SitecoreProvider
{
    public static class WebhookManagementEndpoints
    {
        public class FailureCases : SitecoreProviderTest
        {
            [Theory]
            [InlineData(null)]
            public void NullParameterThrowsArgumentNullException(IEnumerable<string> ids)
            {
                Assert.Throws<ArgumentNullException>(() => Sut.WebhookManagementEndpoints(ids));
            }

            [Fact]
            public void EmptyParameterThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => Sut.WebhookManagementEndpoints(new List<string>()));
            }
        }

        public class PassCases : SitecoreProviderTest
        {
            [Theory]
            [InlineAutoData]
            public void PublicMethodThrowsNotImplementedException(IEnumerable<string> ids)
            {
                Assert.Throws<NotImplementedException>(() => Sut.WebhookManagementEndpoints(ids));
            }
        }
    }
}
