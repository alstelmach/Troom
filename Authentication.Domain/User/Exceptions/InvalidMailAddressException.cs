using System;
using Core.Domain.Abstractions.BuildingBlocks;

namespace Authentication.Domain.User.Exceptions
{
    [Serializable]
    public sealed class InvalidMailAddressException : DomainException
    {
        private const string MessagePattern = "{0} is not a valid mail address";
        
        public InvalidMailAddressException(string mailAddress)
            : base(string.Format(MessagePattern, mailAddress))
        {
        }
    }
}
