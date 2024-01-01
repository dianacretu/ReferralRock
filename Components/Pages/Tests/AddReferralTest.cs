using Bunit;
using Moq;
using ReferralRock.Model;
using ReferralRock.Pages;
using System.Diagnostics.Metrics;
using System.Numerics;
using Xunit;

namespace ReferralRock.Components.Pages.Tests
{
    public class AddReferralTest
    {
        public MemberApiResponse CreateMemberApiResponseMock()
        {

            var mockedMembers = new List<Member>
            {
                new Member { Id = "1", DisplayName = "John Doe", FirstName = "John", Email="john.doe@example.com", ReferralCode = "123" },
            };

            var mockApiResponse = new MemberApiResponse
            {
                Offset = 1,
                Total = 1,
                Message = "Succes",
                Members = mockedMembers

            };

            return mockApiResponse;
        }

        public NewReferralApiResponse CreateNewReferralApiResponseMock()
        {
            var mockApiResponse = new NewReferralApiResponse
            {
                Message = "Succes",
                Referral = new Referral
                {
                    FirstName = "John",
                    LastName = "Doe",
                }

            };

            return mockApiResponse;
        }

        [Fact]
        public async Task TestAddReferralRender()
        {
            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetMembers("id", null, null)).ReturnsAsync(CreateMemberApiResponseMock());
            mockMyService.Setup(x => x.CreateReferral(It.IsAny<NewReferral>(), "id")).ReturnsAsync(CreateNewReferralApiResponseMock());

            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);

            var cut = ctx.RenderComponent<AddReferral>(parameters => parameters
                .Add(p => p.MemberId, "id")
            );

            cut.Find("#firstName").Change("John");
            cut.Find("#lastName").Change("Doe");

            cut.Find("button[type='submit']").Click();

            Assert.ThrowsAny<ElementNotFoundException>(() => cut.Find(".alert.alert-danger"));
        }
    }
}
