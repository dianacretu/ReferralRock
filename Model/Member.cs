namespace ReferralRock.Model
{
    public class MemberApiResponse
    {
        public int Offset { get; set; }
        public int Total { get; set; }
        public string Message { get; set; }
        public List<Member> Members { get; set; }
    }

    public class Member
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ExternalIdentifier { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string CountrySubdivision { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool DisabledFlag { get; set; }
        public string CustomOption1Name { get; set; }
        public string CustomOption1Value { get; set; }
        public string CustomText1Name { get; set; }
        public string CustomText1Value { get; set; }
        public string CustomText2Name { get; set; }
        public string CustomText2Value { get; set; }
        public string ProgramId { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramName { get; set; }
        public string ReferralUrl { get; set; }
        public string ReferralCode { get; set; }
        public string MemberUrl { get; set; }
        public int EmailShares { get; set; }
        public int SocialShares { get; set; }
        public int Views { get; set; }
        public int Referrals { get; set; }
        public DateTime? LastShare { get; set; }
        public int ReferralsApproved { get; set; }
        public int ReferralsQualified { get; set; }
        public int ReferralsPending { get; set; }
        public decimal ReferralsApprovedAmount { get; set; }
        public decimal RewardsPendingAmount { get; set; }
        public decimal RewardsIssuedAmount { get; set; }
        public decimal RewardAmountTotal { get; set; }
        public int Rewards { get; set; }
        public DateTime CreateDt { get; set; }
        public string UtmSource { get; set; }
        public string UtmMedium { get; set; }
        public string UtmCampaign { get; set; }
        public string BrowserReferrerUrl { get; set; }
        public string LastViewIPAddress { get; set; }
    }
}
