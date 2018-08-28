using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace IntegrationTests
{
    public static class EncryptionKeys
    {
        public static RSA ReadAsRsa()
        {
            string privateKeyPem = File.ReadAllText("Files/private.pem");
            PemReader pemReader = new PemReader(new StringReader(privateKeyPem));
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair) pemReader.ReadObject();
            RSAParameters parameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
            RSA rsa = RSA.Create(parameters);
            return rsa;
        }
    }
}
