using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Quickstart
{
    public static class EncryptionKeyUtility
    {
        public static string SerializeRsaParameters(string key)
        {
            PemReader pemReader = new PemReader(new StringReader(key));
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
            RSAParameters parameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
            return JsonConvert.SerializeObject((EncryptionKeyParameters)parameters);
        }

        public static RSA GetRsaKeyFromJson(string json) =>
            RSA.Create((RSAParameters)JsonConvert.DeserializeObject<EncryptionKeyParameters>(json));
    }
}
