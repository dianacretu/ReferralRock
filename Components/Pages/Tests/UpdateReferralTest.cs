﻿using Bunit;
using Moq;
using ReferralRock.Model;
using ReferralRock.Pages;
using System.Diagnostics.Metrics;
using System.Numerics;
using Xunit;

namespace ReferralRock.Components.Pages.Tests
{
    public class UpdateReferralTest
    {
        public ReferralApiResponse CreateReferralApiResponseMock()
        {

            var mockedReferrals = new List<Referral>
            {
                new Referral { Id = "1", DisplayName = "John Doe", FirstName = "John", Email="john.doe@example.com" },
            };

            var mockApiResponse = new ReferralApiResponse
            {
                Offset = 1,
                Total = 1,
                Message = "Succes",
                Referrals = mockedReferrals

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

        public UpdateQueryResponse UpdateQueryResponseMock()
        {
            var mockApiResponse = new UpdateQueryResponse
            {
                query = null,
                resultInfo = new ResultInfo
                {
                    Status = "Succeeded",
                    Message = "Succeeded"
                }

            };

            return mockApiResponse;
        }

        [Fact]
        public async Task TestUpdateReferralRender()
        {
            var mockMyService = new Mock<IPageModelWithHttpClient>();
            mockMyService.Setup(x => x.GetReferrals("id_member","id_referral", null, null)).ReturnsAsync(CreateReferralApiResponseMock());
            mockMyService.Setup(x => x.UpdateReferral("id_referral", It.IsAny<Referral>())).ReturnsAsync(UpdateQueryResponseMock());

            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IPageModelWithHttpClient>(mockMyService.Object);

            var cut = ctx.RenderComponent<UpdateReferral>(parameters => parameters
                .Add(p => p.MemberId, "id_member")
                .Add(p => p.ReferralId, "id_referral")
            );

            cut.Find("#firstName").Change("John");
            cut.Find("#lastName").Change("Doe");

            cut.Find("button[type='submit']").Click();

            Assert.ThrowsAny<ElementNotFoundException>(() => cut.Find(".alert.alert-danger"));
        }
    }
}
