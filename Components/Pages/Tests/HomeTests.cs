using Bunit;
using Microsoft.AspNetCore.Components;
using Moq;
using ReferralRock.Model;
using Xunit;

namespace ReferralRock.Components.Pages.Tests
{
    public class HomeTest
    {
        public MemberApiResponse CreateMemberApiResponseMock()
        {

            var mockedMembers = new List<Member>
            {
                new Member { Id = "1", DisplayName = "John Doe", FirstName = "John", Email="john.doe@example.com" },
                new Member { Id = "2", DisplayName = "Jane Doe", FirstName = "Jane" },
            };

            var mockApiResponse = new MemberApiResponse
            {
                Offset = 1,
                Total = 10,
                Message = "Success",
                Members = mockedMembers

            };

            return mockApiResponse;
        }

        [Fact]
        public async Task TestHomeRenderSuccess()
        {
            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetMembers(null, 0, 5)).ReturnsAsync(CreateMemberApiResponseMock());

            var navigationManagerMock = new Mock<NavigationManager>();

            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);
            ctx.Services.AddSingleton(navigationManagerMock.Object);

            var cut = ctx.RenderComponent<Home>();

            // Assert that the table exists
            Assert.NotNull(cut.Find("table"));

            // Assert content of the table
            Assert.Equal("John Doe", cut.Find("table tbody tr:nth-child(1) td:nth-child(2)").TextContent);
            Assert.Equal("Jane Doe", cut.Find("table tbody tr:nth-child(2) td:nth-child(2)").TextContent);

            Assert.NotNull(cut.Find("a[href^='/referrals/']"));

            Assert.ThrowsAny<ElementNotFoundException>(() => cut.Find(".alert.alert-danger"));
        }

        [Fact]
        public async Task TestHomeRenderError()
        {
            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetMembers(null, 0, 5)).ReturnsAsync(new MemberApiResponse
            {
                Offset = 1,
                Total = 0,
                Message = "Fail"
            });

            var navigationManagerMock = new Mock<NavigationManager>();

            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);
            ctx.Services.AddSingleton(navigationManagerMock.Object);

            var cut = ctx.RenderComponent<Home>();

            Assert.NotNull(cut.Find(".alert.alert-danger"));
        }
    }
}
