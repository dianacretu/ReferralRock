namespace ReferralRock.Model
{
    public class PrimaryInfo
    {
        public string referralId { get; set; }
    }
    public class SecondaryInfo
    {
        public string externalIdentifier { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
    public class TertiaryInfo
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramTitle { get; set; }
    }
    public class FuzzyInfo
    {
        public string Identifier { get; set; }
    }
    public class Info
    {
        public PrimaryInfo primaryInfo { get; set; }
        public SecondaryInfo secondaryInfo { get; set; }
        public TertiaryInfo tertiaryInfo { get; set; }
        public FuzzyInfo fuzzyInfo { get; set; }
    }

    public class DeleteQuery
    {
        public Info query { get; set; }
    }
    public class UpdateQuery
    {
        public Info query { get; set; }
        public Referral referral { get; set; }
    }
    public class ResultInfo 
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class UpdateQueryResponse
    {
        public Info query { get; set; }
        public Referral referral { get; set; }
        public ResultInfo resultInfo { get; set; }
    }
}
