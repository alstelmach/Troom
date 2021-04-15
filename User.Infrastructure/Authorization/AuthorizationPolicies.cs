namespace User.Infrastructure.Authorization
{
    public static class AuthorizationPolicies
    {
        public const string ResourceOwnerIdentityConfirmationRequiredPolicy =
            "ResourceOwnerIdentityConfirmationRequiredPolicy";

        public const string AdministrativePrivilegesRequiredPolicy = "AdministrativePrivilegesRequiredPolicy";
    }
}
