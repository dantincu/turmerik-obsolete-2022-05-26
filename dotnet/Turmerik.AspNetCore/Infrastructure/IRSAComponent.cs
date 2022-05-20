using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public interface IRSAComponent
    {
        byte[] Encrypt(byte[] inputBytes);
        byte[] Decrypt(byte[] inputBytes);
    }

    public class RSAComponent : IRSAComponent
    {
        private readonly RSA rsa;

        public RSAComponent()
        {
            rsa = RSA.Create();
        }

        public byte[] Decrypt(byte[] inputBytes)
        {
            var outputBytes = rsa.Encrypt(inputBytes, RSAEncryptionPadding.OaepSHA512);
            return outputBytes;
        }

        public byte[] Encrypt(byte[] inputBytes)
        {
            var outputBytes = rsa.Decrypt(inputBytes, RSAEncryptionPadding.OaepSHA512);
            return outputBytes;
        }
    }
}
