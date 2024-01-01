using Bunit;
using Moq;
using ReferralRock.Model;
using ReferralRock.Pages;
using System.Diagnostics.Metrics;
using System.Numerics;
using Xunit;

namespace ReferralRock.Components.Pages.Tests
{
    public class ReferralsTest
    {
        [Fact]
        public async Task TestReferralsRenderSuccess()
        {
            var mockedReferrals = new List<Referral>
            {
                new Referral { Id = "1", DisplayName = "John Doe", FirstName = "John" },
                new Referral { Id = "2", DisplayName = "Jane Doe", FirstName = "Jane" },
            };

            var mockApiResponse = new ReferralApiResponse
            {
                Offset = 1,
                Total = 10,
                Message = "Succes",
                Referrals = mockedReferrals

            };

            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetReferrals("id", null, 0, 5)).ReturnsAsync(mockApiResponse);
            
            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);

            var cut = ctx.RenderComponent<Referrals>(parameters => parameters
                .Add(p => p.MemberId, "id")
            );

            // Assert that the table exists
            Assert.NotNull(cut.Find("table"));

            // Assert content of the table
            Assert.Equal("John Doe", cut.Find("table tbody tr:nth-child(1) td:nth-child(2)").TextContent);
            Assert.Equal("Jane Doe", cut.Find("table tbody tr:nth-child(2) td:nth-child(2)").TextContent);

            // Assert the presence of a button in the first row for updating
            Assert.NotNull(cut.Find("table tbody tr:nth-child(1) button:contains('Update')"));

            // Assert the presence of a button in the first row for deleting
            Assert.NotNull(cut.Find("table tbody tr:nth-child(1) button:contains('Delete')"));

            Assert.ThrowsAny<ElementNotFoundException>(() => cut.Find(".alert.alert-danger"));
        }
    }
}
