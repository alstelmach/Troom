using System;

namespace User.Application.Services
{
    public interface ITokenGenerationService
    {
        string GenerateToken(Guid ownerId);
    }
}
