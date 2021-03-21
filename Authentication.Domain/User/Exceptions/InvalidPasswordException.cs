using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace Authentication.Domain.User.Exceptions
{
    [Serializable]
    public sealed class InvalidPasswordException : DomainException
    {
        private new const string Message =
            "Password must be at least 8 characters long, must contain a digit, a capital english letter, lower case english letter, and one special character";
        
        public InvalidPasswordException()
            : base(Message)
        {
        }
    }
}
