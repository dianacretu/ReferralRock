namespace ReferralRock.Model
{
    public class PrimaryInfo
    {
        public string referralId { get; set; }
    }
    public class Info
    {
        public PrimaryInfo primaryInfo { get; set; }
    }

    public class Query
    {
        public Info query { get; set; }
    }
}
