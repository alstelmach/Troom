namespace User.Application.Dto
{
    public sealed record AuthenticationResultDto(
        bool IsAuthenticated,
        string JsonWebToken,
        UserDto TokenOwner);
}
