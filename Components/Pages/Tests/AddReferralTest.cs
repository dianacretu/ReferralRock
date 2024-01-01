using Bunit;
using Moq;
using ReferralRock.Model;
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
                Message = "Success",
                Referral = new ReceivedReferral
                {
                    FirstName = "John",
                    LastName = "Doe",
                }

            };

            return mockApiResponse;
        }

        [Fact]
        public async Task TestAddReferralRenderSuccess()
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

        [Fact]
        public async Task TestAddReferralRenderValidationError()
        {
            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetMembers("id", null, null)).ReturnsAsync(CreateMemberApiResponseMock());
            mockMyService.Setup(x => x.CreateReferral(It.IsAny<NewReferral>(), "id")).ReturnsAsync(CreateNewReferralApiResponseMock());

            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);

            var cut = ctx.RenderComponent<AddReferral>(parameters => parameters
                .Add(p => p.MemberId, "id")
            );

            cut.Find("#firstName").Change(".....");
            cut.Find("#lastName").Change("Doe");

            cut.Find("button[type='submit']").Click();

            Assert.Contains("Invalid first name", cut.Markup);
        }

        [Fact]
        public async Task TestAddReferralRenderError()
        {
            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetMembers("id", null, null)).ReturnsAsync(CreateMemberApiResponseMock());
            mockMyService.Setup(x => x.CreateReferral(It.IsAny<NewReferral>(), "id")).ReturnsAsync(new NewReferralApiResponse
            {
                Message = "Fail"
            });

            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);

            var cut = ctx.RenderComponent<AddReferral>(parameters => parameters
                .Add(p => p.MemberId, "id")
            );

            cut.Find("#firstName").Change("John");
            cut.Find("#lastName").Change("Doe");

            cut.Find("button[type='submit']").Click();

            Assert.NotNull(cut.Find(".alert.alert-danger"));
        }
    }
}
