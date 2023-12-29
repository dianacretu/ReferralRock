namespace ReferralRock.Model
{
    public class MemberApiResponse<Member>
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
    }
}
