using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Authentication.Domain.User.Services;
using Microsoft.Extensions.Options;

namespace Authentication.Infrastructure.Services.Encryption
{
    public sealed class EncryptionService : IEncryptionService
    {
        private readonly EncryptionSettings _encryptionSettings;

        public EncryptionService(IOptions<EncryptionSettings> encryptionSettings)
        {
            _encryptionSettings = encryptionSettings.Value;
        }
        
        public bool VerifyPassword(byte[] passwordHash, string password)
        {
            var salt = GetStoredPasswordSalt(passwordHash);
            var passedPasswordHash = HashPassword(password, salt);
            var isCorrect = VerifyEncryptedBytes(passwordHash, passedPasswordHash);

            return isCorrect;
        }

        public byte[] EncodePassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var salt = new byte[_encryptionSettings.SaltLength];
            cryptoProvider.GetBytes(salt);

            var coreHash = HashPassword(password, salt);
            var passwordHash = new byte[_encryptionSettings.PasswordHashLength];
            
            Array.Copy(salt, 0, passwordHash, 0, _encryptionSettings.SaltLength);
            Array.Copy(coreHash, 0, passwordHash, _encryptionSettings.SaltLength, coreHash.Length);

            return passwordHash;
        }

        private bool VerifyEncryptedBytes(IReadOnlyList<byte> storedPasswordHash,
            IReadOnlyList<byte> passwordHash)
        {
            var matchingBytesCount = 0;
            var bytesToCheckCount =
                _encryptionSettings.PasswordHashLength - _encryptionSettings.SaltLength;

            for (var i = 0; i < bytesToCheckCount; i++)
            {
                var doesMatch = storedPasswordHash[i + _encryptionSettings.SaltLength] == passwordHash[i];
                
                if (doesMatch)
                {
                    matchingBytesCount++;
                }
            }

            var isMatched = matchingBytesCount
                == _encryptionSettings.PasswordHashLength - _encryptionSettings.SaltLength;
            
            return isMatched;
        }

        private byte[] HashPassword(string passedPassword, byte[] salt)
        {
            var coreHashLength = _encryptionSettings.PasswordHashLength - _encryptionSettings.SaltLength;
            var key = new Rfc2898DeriveBytes(passedPassword,
                salt,
                _encryptionSettings.HashIterationsCount);

            var hashedPassword = key
                .GetBytes(coreHashLength);

            return hashedPassword;
        }

        private byte[] GetStoredPasswordSalt(byte[] storedPasswordHash)
        {
            var salt = new byte[_encryptionSettings.SaltLength];
            
            Array.Copy(storedPasswordHash,
                0,
                salt,
                0,
                _encryptionSettings.SaltLength);

            return salt;
        }
    }
}