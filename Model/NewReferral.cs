using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReferralRock.Model
{
    public class NewReferralApiResponse
    {
        public string Message { get; set; }
        public ReceivedReferral Referral { get; set; }
    }

    public class NewReferral
    {
        public string ReferralCode { get; set; }

        [BindProperty]
        [MaxLength(50, ErrorMessage = "First name should not be longer than 50 characters.")]
        [RegularExpression("^[A-Za-z\\-\\'\\s]+$", ErrorMessage = "Invalid first name")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last name should not be longer than 50 characters.")]
        [RegularExpression("^[A-Za-z\\-\\'\\s]+$", ErrorMessage = "Invalid last name")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Email should not be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9._+-]+@([a-zA-Z0-9._-]+)\\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [MaxLength(21, ErrorMessage = "Phone number should not be longer than 50 characters.")]
        [RegularExpression("^(\\+)?[0-9]{5,20}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [MaxLength(13, ErrorMessage = "Preferred contact should not be longer than 50 characters.")]
        [RegularExpression("^(email|callMorning|callAfternoon|callEvening)$", ErrorMessage = "Invalid value. Preferred contact can be 'email', 'callMorning', 'callAfternoon', or 'callEvening'.")]
        public string PreferredContact { get; set; }

        [MaxLength(50, ErrorMessage = "Identifier should not be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z_][a-zA-Z0-9_-]*$", ErrorMessage = "Invalid identifier")]
        public string ExternalIdentifier { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = " Invalid amount")]
        public string Amount { get; set; }

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

        [MaxLength(50, ErrorMessage = "Custom option1 value should not be longer than 50 characters.")]
        public string CustomOption1Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom option2 value should not be longer than 50 characters.")]
        public string CustomOption2Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text1 value should not be longer than 50 characters.")]
        public string CustomText1Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text2 value should not be longer than 50 characters.")]
        public string CustomText2Value { get; set; }

        [MaxLength(50, ErrorMessage = "Custom text3 value should not be longer than 50 characters.")]
        public string CustomText3Value { get; set; }

        [MaxLength(13, ErrorMessage = "Status should not be longer than 50 characters.")]
        [RegularExpression("^(pending|qualified|approved|denied)$", ErrorMessage = "Invalid value. Status can be 'pending', 'qualified', 'approved', or 'denied'.")]
        public string Status { get; set; }

        public void FromReceivedReferral(ReceivedReferral referral)
        {
            FirstName = referral.FirstName;
            LastName = referral.LastName;
            Email = referral.Email;
            PhoneNumber = referral.PhoneNumber;
            PreferredContact = referral.PreferredContact;
            ExternalIdentifier = referral.ExternalIdentifier;
            Amount = referral.Amount.ToString();
            CompanyName = referral.CompanyName;
            Note = referral.Note;
            PublicNote = referral.PublicNote;
            CustomOption1Name = referral.CustomOption1Name;
            CustomOption2Name = referral.CustomOption2Name;
            CustomText1Name = referral.CustomText1Name;
            CustomText2Name = referral.CustomText2Name;
            CustomText3Name = referral.CustomText3Name;
            CustomOption1Value = referral.CustomOption1Value;
            CustomOption2Value = referral.CustomOption2Value;
            CustomText1Value = referral.CustomText1Value;
            CustomText2Value = referral.CustomText2Value;
            CustomText3Value = referral.CustomText3Value;
        }
    }

}
