using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Exceptions
{
    [Serializable]
    public sealed class UserLoginUniquenessException : DomainException
    {
        private const string MessagePattern = "Login {0} is already taken!";
        
        public UserLoginUniquenessException(string message)
            : base(string.Format(MessagePattern, message))
        {
        }
    }
}
