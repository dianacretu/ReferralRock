namespace ReferralRock.Model
{
    public class NewReferralApiResponse
    {
        public string Message { get; set; }
        public Referral Referral { get; set; }
    }

    public class NewReferral
    {
        public string ReferralCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PreferredContact { get; set; }
        public string ExternalIdentifier { get; set; }
        public int Amount { get; set; }
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
    }

}
