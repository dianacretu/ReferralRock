using ReferralRock.Model;

public interface IPageModelWithHttpClient
{
    Task<MemberApiResponse> GetMembers(string? memberId = null, int? offset = null, int? count = null);
    Task<ReferralApiResponse> GetReferrals(string memberId, string? query = null, int? offset = null, int? count = null);
    Task<NewReferralApiResponse> CreateReferral(NewReferral referral, string memberId);
    Task<DeleteApiResponse> DeleteReferral(string referralId, string memberId);
    Task<UpdateQueryResponse> UpdateReferral(string referralId, ReceivedReferral referral);
}
