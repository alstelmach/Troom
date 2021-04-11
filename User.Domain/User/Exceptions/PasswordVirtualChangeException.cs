using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace User.Domain.User.Exceptions
{
    [Serializable]
    public sealed class PasswordVirtualChangeException : DomainException
    {
        private new const string Message = "Password must be the different than currently set";
        
        public PasswordVirtualChangeException()
            : base(Message)
        {
        }
    }
}
