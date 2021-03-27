using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Exceptions
{
    [Serializable]
    public sealed class UserMailAddressUniquenessException : DomainException
    {
        private const string MessagePattern = "Mail address {0} is already taken!";
        
        public UserMailAddressUniquenessException(string message)
            : base(string.Format(MessagePattern, message))
        {
        }
    }
}
