namespace ReferralRock.Model
{
    public class ReferralApiResponse
    {
        public int Offset { get; set; }
        public int Total { get; set; }
        public string Message { get; set; }
        public List<Referral> Referrals { get; set; }

    }
    public class Referral
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ExternalIdentifier { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string AmountFormatted { get; set; }
        public string PreferredContact { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Source { get; set; }
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramTitle { get; set; }
        public string ReferringMemberId { get; set; }
        public string ReferringMemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberReferralCode { get; set; }
        public string MemberExternalIdentifier { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? QualifiedDate { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
        public string Note { get; set; }
        public string PublicNote { get; set; }
        public string CustomOption1Name { get; set; }
        public string CustomOption2Name { get; set; }
        public string CustomText1Name { get; set; }
        public string CustomText2Name { get; set; }
        public string CustomText3Name { get; set; }
        public string CustomOption1Value { get; set; }
        public string CustomOption2Value { get; set; }
        public string CustomText1Value { get; set; }
        public string CustomText2Value { get; set; }
        public string CustomText3Value { get; set; }
        public string ConversionNote { get; set; }
        public string ConversionIPAddress { get; set; }
        public string UtmSource { get; set; }
        public string UtmMedium { get; set; }
        public string UtmCampaign { get; set; }
        public string BrowserReferrerUrl { get; set; }
        public string IPAddressSource { get; set; }
    }
}
