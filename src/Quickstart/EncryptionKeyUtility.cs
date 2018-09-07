using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Quickstart
{
    /// <summary>
    /// Provides utilities for serialization of an RSA key parameters
    /// </summary>
    public static class EncryptionKeyUtility
    {
        /// <summary>
        /// Serializes parameters of an RSA key in PEM format to JSON
        /// </summary>
        /// <param name="key">RSA private key in PEM format</param>
        /// <returns>JSON string representing RSA parameters of the <paramref name="key"/></returns>
        public static string SerializeRsaParameters(string key)
        {
            PemReader pemReader = new PemReader(new StringReader(key));
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair) pemReader.ReadObject();
            RSAParameters parameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
            return JsonConvert.SerializeObject((EncryptionKeyParameters) parameters);
        }

        /// <summary>
        /// Creates an instance of <see cref="RSA"/> from key parameters serialized in JSON.
        /// Use <see cref="SerializeRsaParameters"/> method to generate the expected JSON string.
        /// </summary>
        /// <param name="json">JSON-serialized RSA key parameters</param>
        /// <returns>Created RSA instance representing the encryption key</returns>
        public static RSA GetRsaKeyFromJson(string json) =>
            RSA.Create((RSAParameters) JsonConvert.DeserializeObject<EncryptionKeyParameters>(json));
    }
}
