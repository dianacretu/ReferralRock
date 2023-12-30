using System.ComponentModel.DataAnnotations;

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

        [MaxLength(50, ErrorMessage = "First name should not be longer than 50 characters.")]
        [RegularExpression("^[A-Za-z\\-\\'\\s]+$", ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last name should not be longer than 50 characters.")]
        [RegularExpression("^[A-Za-z\\-\\'\\s]+$", ErrorMessage = "Invalid last name")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Email should not be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9._+-]+@([a-zA-Z0-9._-]+)\\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "Identifier should not be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z_][a-zA-Z0-9_-]*$", ErrorMessage = "Invalid identifier.")]
        public string ExternalIdentifier { get; set; }

        [MaxLength(21, ErrorMessage = "Phone number should not be longer than 50 characters.")]
        [RegularExpression("^(\\+)?[0-9]{5,20}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage =" Invalid amount")]
        public decimal Amount { get; set; }

        public string AmountFormatted { get; set; }

        [MaxLength(13, ErrorMessage = "Preferred contact should not be longer than 50 characters.")]
        [RegularExpression("^(email|callMorning|callAfternoon|callEvening)$", ErrorMessage = "Invalid value. Preferred contact can be 'email', 'callMorning', 'callAfternoon', or 'callEvening'.")]
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

        [MaxLength(13, ErrorMessage = "Status should not be longer than 50 characters.")]
        [RegularExpression("^(pending|qualified|approved|denied)$", ErrorMessage = "Invalid value. Status can be 'pending', 'qualified', 'approved', or 'denied'.")]
        public string Status { get; set; }

        [MaxLength(50, ErrorMessage = "Company name should not be longer than 50 characters.")]
        [RegularExpression("^[A-Za-z\\-\\'\\s]+$", ErrorMessage = "Invalid company name")]
        public string CompanyName { get; set; }

        [MaxLength(50, ErrorMessage = "Note should not be longer than 50 characters.")]
        public string Note { get; set; }

        [MaxLength(50, ErrorMessage = "Public note should not be longer than 50 characters.")]
        public string PublicNote { get; set; }
        
        [MaxLength(50, ErrorMessage = "Custom option1 name should not be longer than 50 characters.")]
        public string CustomOption1Name { get; set; }

        [MaxLength(50, ErrorMessage = "Custom option2 name should not be longer than 50 characters.")]
        public string CustomOption2Name { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text1 name should not be longer than 50 characters.")]
        public string CustomText1Name { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text2 name should not be longer than 50 characters.")]
        public string CustomText2Name { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text3 name should not be longer than 50 characters.")]
        public string CustomText3Name { get; set; }

        [MaxLength(50, ErrorMessage = "Custom option1 vlue should not be longer than 50 characters.")]
        public string CustomOption1Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom option2 vlue should not be longer than 50 characters.")]
        public string CustomOption2Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text1 value should not be longer than 50 characters.")]
        public string CustomText1Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text2 value should not be longer than 50 characters.")]
        public string CustomText2Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text3 value should not be longer than 50 characters.")]
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
