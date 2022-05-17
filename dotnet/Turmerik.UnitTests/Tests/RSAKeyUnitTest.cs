using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Helpers;
using Xunit;

namespace Turmerik.UnitTests.Tests
{
    public class RSAKeyUnitTest : UnitTestBase
    {
        [Fact]
        public void MainTest()
        {
            //Generate a public/private key pair.  
            RSA rsa = RSA.Create();
            //Save the public key information to an RSAParameters structure.  
            var publicKey = rsa.ExportRSAPublicKey();
            var privateKey = rsa.ExportRSAPrivateKey();

            string testStr = "some.user@some.domain.com";
            var testBytes = EncodeH.EncodeSha1(testStr);
        }
    }
}
