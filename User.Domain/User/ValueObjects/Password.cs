using AsCore.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.ValueObjects
{
    public sealed record Password : ValueObject
    {
        public Password(string password) =>
            Content = password;

        public string Content { get; }
    }
}
