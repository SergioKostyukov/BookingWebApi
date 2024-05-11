namespace Booking.WebApi.Identity;

public class IdentityConstants
{
    public const string AdminUserClaimName = "Admin";
    public const string ManagerUserClaimName = "Manager";
    public const string ClientUserClaimName = "Client";

    public const string AdminUserPolicyName = "Admin";
    public const string ManagerUserPolicyName = "Manager";
    public const string ClientUserPolicyName = "Client";
    public const string ClientOrManagerUserPolicyName = "ClientOrManagerPolicy";
}
