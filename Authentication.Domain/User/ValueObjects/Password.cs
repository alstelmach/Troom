using System.Collections.Generic;
using Core.Domain.Abstractions.BuildingBlocks;

namespace Authentication.Domain.User.ValueObjects
{
    public class Password : ValueObject
    {
        public Password(string password) =>
            Content = password;

        public string Content { get; }

        protected override IEnumerable<object> GetEqualityComponents() =>
            new[] { Content };
    }
}
