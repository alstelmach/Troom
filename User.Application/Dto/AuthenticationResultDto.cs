namespace User.Application.Dto
{
    public sealed class AuthenticationResultDto
    {
        public bool IsAuthenticated { get; set; }
        public string JsonWebToken { get; set; }
        public UserDto TokenOwner { get; set; }
    }
}
