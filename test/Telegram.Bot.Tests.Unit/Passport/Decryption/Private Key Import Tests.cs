using System;
using System.Reflection;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Xunit;
using Xunit.Abstractions;

namespace Telegram.Bot.Tests.Unit.Passport.Decryption
{
    /// <summary>
    /// Tests for importing private key and decrypting credentials
    /// </summary>
    public class PrivateKeyImportTests
    {
        private readonly ITestOutputHelper _output;

        public PrivateKeyImportTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "Should read private RSA key parameters from PEM format")]
        public void Should_Read_RSA_Parameters_From_Pem()
        {
            byte[] d =
            {
                158, 181, 196, 120, 233, 118, 43, 45, 226, 30, 237, 58, 239, 219, 42, 37, 24, 52, 101, 153, 240, 51,
                125, 147, 167, 162, 5, 196, 209, 137, 17, 126, 139, 139, 43, 38, 248, 105, 119, 24, 254, 28, 193, 184,
                188, 180, 42, 53, 13, 163, 220, 236, 115, 88, 228, 233, 125, 2, 76, 129, 212, 127, 173, 13, 156, 147,
                202, 191, 141, 213, 244, 145, 212, 30, 154, 39, 1, 243, 219, 102, 186, 93, 71, 26, 189, 203, 117, 89,
                58, 142, 137, 200, 56, 167, 102, 43, 245, 220, 127, 253, 31, 155, 146, 157, 77, 68, 168, 103, 160, 120,
                190, 215, 128, 70, 214, 41, 154, 44, 30, 191, 37, 15, 38, 187, 5, 25, 99, 197, 32, 252, 163, 12, 78, 37,
                74, 41, 150, 65, 82, 18, 180, 63, 79, 252, 212, 136, 252, 5, 190, 110, 59, 174, 64, 191, 69, 201, 207,
                62, 121, 97, 215, 180, 247, 142, 14, 100, 163, 89, 44, 243, 58, 168, 247, 157, 209, 115, 233, 64, 87,
                64, 234, 56, 212, 103, 57, 250, 110, 51, 73, 172, 73, 218, 7, 165, 26, 53, 118, 178, 209, 193, 66, 29,
                135, 234, 36, 63, 155, 147, 35, 140, 71, 117, 99, 41, 143, 37, 118, 1, 45, 218, 44, 57, 29, 135, 158,
                105, 212, 137, 151, 59, 214, 224, 35, 80, 255, 27, 175, 9, 247, 112, 253, 62, 190, 217, 163, 205, 223,
                228, 103, 94, 94, 2, 110, 228, 9, 33
            };
            byte[] dp =
            {
                41, 150, 25, 89, 187, 50, 253, 186, 9, 73, 45, 217, 240, 222, 109, 145, 224, 53, 245, 202, 185, 224,
                170, 210, 141, 147, 137, 18, 114, 73, 226, 220, 142, 81, 202, 255, 220, 5, 243, 139, 19, 128, 54, 166,
                239, 43, 120, 200, 131, 250, 154, 76, 241, 205, 56, 39, 85, 85, 239, 43, 88, 73, 22, 63, 47, 38, 216,
                53, 222, 194, 203, 116, 16, 254, 114, 37, 14, 10, 99, 239, 159, 70, 24, 107, 247, 5, 79, 33, 96, 98, 25,
                153, 224, 244, 117, 238, 213, 188, 194, 11, 185, 61, 10, 224, 187, 113, 201, 225, 40, 221, 35, 97, 26,
                48, 222, 95, 193, 154, 102, 125, 222, 61, 90, 22, 191, 229, 147, 173
            };
            byte[] dq =
            {
                99, 110, 74, 131, 51, 227, 71, 65, 35, 247, 77, 147, 32, 213, 249, 18, 59, 168, 84, 106, 244, 193, 218,
                103, 96, 45, 176, 236, 7, 158, 27, 4, 234, 188, 15, 194, 161, 120, 91, 247, 239, 189, 244, 57, 98, 124,
                38, 70, 126, 108, 246, 98, 190, 221, 104, 111, 240, 19, 52, 140, 128, 175, 223, 72, 69, 39, 78, 68, 172,
                86, 103, 104, 158, 148, 58, 231, 168, 80, 51, 71, 1, 232, 77, 251, 150, 106, 209, 125, 28, 233, 135, 58,
                122, 83, 127, 250, 103, 5, 152, 172, 120, 23, 3, 159, 199, 193, 125, 197, 26, 46, 161, 61, 230, 143,
                160, 31, 91, 56, 101, 135, 21, 180, 8, 80, 176, 29, 5, 221
            };
            byte[] exponent = { 1, 0, 1 };
            byte[] inverseQ =
            {
                211, 94, 119, 169, 124, 20, 144, 195, 119, 21, 4, 101, 88, 143, 6, 193, 197, 12, 104, 47, 209, 129, 81,
                51, 33, 141, 22, 144, 126, 165, 1, 88, 98, 85, 49, 38, 177, 137, 191, 186, 116, 6, 218, 48, 128, 21,
                195, 64, 200, 48, 161, 188, 181, 54, 13, 18, 49, 41, 200, 107, 135, 189, 40, 109, 226, 246, 81, 129,
                170, 194, 54, 160, 228, 215, 113, 10, 99, 205, 11, 147, 35, 175, 216, 127, 201, 211, 90, 237, 129, 166,
                100, 235, 225, 250, 217, 110, 115, 26, 114, 27, 183, 58, 29, 7, 105, 251, 37, 171, 10, 231, 173, 72,
                115, 244, 28, 172, 75, 97, 120, 46, 16, 154, 191, 145, 93, 237, 55, 166
            };
            byte[] mod =
            {
                209, 81, 37, 90, 132, 0, 217, 34, 181, 114, 193, 182, 254, 198, 63, 194, 91, 44, 59, 86, 227, 93, 28,
                126, 183, 226, 101, 34, 4, 186, 140, 179, 194, 121, 252, 130, 1, 198, 65, 191, 177, 37, 113, 35, 201,
                64, 131, 196, 92, 220, 13, 217, 119, 103, 77, 205, 176, 16, 136, 217, 241, 180, 224, 6, 80, 106, 94,
                154, 99, 227, 189, 91, 37, 229, 252, 50, 45, 174, 3, 16, 42, 57, 64, 238, 188, 107, 170, 182, 31, 166,
                171, 176, 204, 170, 137, 119, 48, 209, 249, 63, 209, 78, 234, 88, 187, 157, 156, 72, 212, 85, 247, 246,
                237, 171, 255, 129, 248, 61, 116, 98, 91, 29, 119, 130, 197, 80, 68, 190, 130, 150, 173, 163, 113, 113,
                71, 115, 208, 175, 53, 160, 144, 16, 149, 253, 230, 56, 16, 20, 12, 155, 106, 93, 181, 232, 76, 37, 158,
                117, 85, 130, 36, 255, 76, 219, 31, 22, 53, 184, 213, 114, 89, 176, 117, 39, 203, 217, 62, 116, 7, 242,
                63, 58, 128, 147, 158, 28, 130, 187, 227, 128, 1, 201, 107, 32, 214, 141, 170, 106, 247, 65, 102, 255,
                231, 191, 13, 84, 58, 4, 113, 164, 87, 67, 206, 64, 201, 36, 6, 187, 239, 132, 2, 16, 187, 109, 182,
                143, 237, 55, 126, 185, 189, 111, 108, 153, 174, 21, 255, 169, 45, 53, 151, 40, 214, 225, 86, 144, 202,
                209, 149, 211, 9, 94, 193, 94, 129, 23
            };
            byte[] p =
            {
                231, 163, 29, 139, 11, 6, 106, 184, 190, 126, 22, 155, 215, 249, 90, 10, 34, 95, 154, 93, 7, 64, 251,
                195, 22, 94, 102, 10, 243, 177, 67, 50, 117, 183, 91, 184, 246, 63, 156, 28, 134, 114, 81, 90, 219, 27,
                161, 146, 91, 181, 252, 152, 145, 10, 46, 124, 124, 100, 225, 22, 168, 217, 50, 45, 206, 245, 127, 187,
                146, 91, 43, 69, 218, 5, 185, 171, 38, 60, 224, 149, 16, 224, 180, 184, 81, 124, 250, 158, 120, 201,
                110, 124, 18, 239, 190, 159, 244, 106, 133, 65, 10, 39, 220, 202, 146, 1, 63, 74, 176, 51, 23, 151, 41,
                153, 147, 213, 179, 229, 50, 90, 90, 126, 162, 236, 84, 176, 13, 163
            };
            byte[] q =
            {
                231, 85, 13, 218, 199, 119, 199, 252, 63, 168, 10, 200, 68, 90, 15, 176, 17, 139, 130, 185, 117, 227,
                74, 222, 122, 201, 228, 20, 64, 195, 41, 94, 105, 189, 29, 229, 144, 169, 170, 254, 85, 146, 88, 5, 57,
                19, 216, 149, 3, 103, 46, 249, 38, 97, 74, 50, 150, 167, 52, 239, 90, 88, 230, 9, 111, 243, 134, 168,
                34, 50, 232, 160, 56, 174, 54, 93, 9, 47, 202, 67, 183, 102, 21, 165, 138, 168, 204, 187, 227, 152, 194,
                244, 183, 189, 183, 1, 23, 46, 249, 30, 248, 66, 235, 229, 61, 65, 112, 59, 176, 136, 52, 173, 204, 21,
                195, 77, 17, 195, 202, 40, 111, 67, 248, 39, 142, 13, 77, 253
            };

            RSAParameters parameters;
            {
                PemReader pemReader = new PemReader(new System.IO.StringReader(PrivateKeyPem));
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair) pemReader.ReadObject();
                parameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
            }

            Assert.Equal(d, parameters.D);
            Assert.Equal(exponent, parameters.Exponent);
            Assert.Equal(mod, parameters.Modulus);
            Assert.Equal(p, parameters.P);
            Assert.Equal(q, parameters.Q);
            Assert.Equal(dp, parameters.DP);
            Assert.Equal(dq, parameters.DQ);
            Assert.Equal(inverseQ, parameters.InverseQ);
        }

        [Fact(DisplayName =
            "Should JSON-serialize RSAParameters and check whether fields are serializable on this platform")]
        public void Should_JSON_Serialize_RSAParameters_If_Supported()
        {
            bool areAllFieldsSerializable = typeof(RSAParameters)
                                                .GetField(nameof(RSAParameters.P))!
                                                .GetCustomAttribute<NonSerializedAttribute>() == null;

            if (!areAllFieldsSerializable)
            {
                _output.WriteLine("Some fields of RSAParameters struct are NOT serializable on this platform.");
            }

            string expectedJson = areAllFieldsSerializable
                    ? @"{""D"":""nrXEeOl2Ky3iHu0679sqJRg0ZZnwM32Tp6IFxNGJEX6Liysm+Gl3GP4cwbi8tCo1DaPc7HNY5Ol9AkyB1H+tDZyTyr+N1fSR1B6aJwHz22a6XUcavct1WTqOicg4p2Yr9dx//R+bkp1NRKhnoHi+14BG1imaLB6/JQ8muwUZY8Ug/KMMTiVKKZZBUhK0P0/81Ij8Bb5uO65Av0XJzz55Yde0944OZKNZLPM6qPed0XPpQFdA6jjUZzn6bjNJrEnaB6UaNXay0cFCHYfqJD+bkyOMR3VjKY8ldgEt2iw5HYeeadSJlzvW4CNQ/xuvCfdw/T6+2aPN3+RnXl4CbuQJIQ=="",""DP"":""KZYZWbsy/boJSS3Z8N5tkeA19cq54KrSjZOJEnJJ4tyOUcr/3AXzixOANqbvK3jIg/qaTPHNOCdVVe8rWEkWPy8m2DXewst0EP5yJQ4KY++fRhhr9wVPIWBiGZng9HXu1bzCC7k9CuC7ccnhKN0jYRow3l/BmmZ93j1aFr/lk60="",""DQ"":""Y25KgzPjR0Ej902TINX5EjuoVGr0wdpnYC2w7AeeGwTqvA/CoXhb9++99DlifCZGfmz2Yr7daG/wEzSMgK/fSEUnTkSsVmdonpQ656hQM0cB6E37lmrRfRzphzp6U3/6ZwWYrHgXA5/HwX3FGi6hPeaPoB9bOGWHFbQIULAdBd0="",""Exponent"":""AQAB"",""InverseQ"":""0153qXwUkMN3FQRlWI8GwcUMaC/RgVEzIY0WkH6lAVhiVTEmsYm/unQG2jCAFcNAyDChvLU2DRIxKchrh70obeL2UYGqwjag5NdxCmPNC5Mjr9h/ydNa7YGmZOvh+tlucxpyG7c6HQdp+yWrCuetSHP0HKxLYXguEJq/kV3tN6Y="",""Modulus"":""0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFw=="",""P"":""56MdiwsGari+fhab1/laCiJfml0HQPvDFl5mCvOxQzJ1t1u49j+cHIZyUVrbG6GSW7X8mJEKLnx8ZOEWqNkyLc71f7uSWytF2gW5qyY84JUQ4LS4UXz6nnjJbnwS776f9GqFQQon3MqSAT9KsDMXlymZk9Wz5TJaWn6i7FSwDaM="",""Q"":""51UN2sd3x/w/qArIRFoPsBGLgrl140reesnkFEDDKV5pvR3lkKmq/lWSWAU5E9iVA2cu+SZhSjKWpzTvWljmCW/zhqgiMuigOK42XQkvykO3ZhWliqjMu+OYwvS3vbcBFy75HvhC6+U9QXA7sIg0rcwVw00Rw8oob0P4J44NTf0=""}"
                    : @"{""Exponent"":""AQAB"",""Modulus"":""0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFw==""}"
                ;

            RSAParameters parameters;
            {
                PemReader pemReader = new PemReader(new System.IO.StringReader(PrivateKeyPem));
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair) pemReader.ReadObject();
                parameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
            }
            string json = JsonConvert.SerializeObject(parameters);
            _output.WriteLine("JSON value is:\n" + json);

            Assert.True(JToken.DeepEquals(
                JToken.Parse(expectedJson),
                JToken.Parse(json)
            ));
        }

        [Fact(DisplayName =
            "Should deserialize RSAParameters and check whether fields are serializable on this platform")]
        public void Should_Deserialize_RSAParameters_From_JSON()
        {
            byte[] d =
            {
                158, 181, 196, 120, 233, 118, 43, 45, 226, 30, 237, 58, 239, 219, 42, 37, 24, 52, 101, 153, 240, 51,
                125, 147, 167, 162, 5, 196, 209, 137, 17, 126, 139, 139, 43, 38, 248, 105, 119, 24, 254, 28, 193, 184,
                188, 180, 42, 53, 13, 163, 220, 236, 115, 88, 228, 233, 125, 2, 76, 129, 212, 127, 173, 13, 156, 147,
                202, 191, 141, 213, 244, 145, 212, 30, 154, 39, 1, 243, 219, 102, 186, 93, 71, 26, 189, 203, 117, 89,
                58, 142, 137, 200, 56, 167, 102, 43, 245, 220, 127, 253, 31, 155, 146, 157, 77, 68, 168, 103, 160, 120,
                190, 215, 128, 70, 214, 41, 154, 44, 30, 191, 37, 15, 38, 187, 5, 25, 99, 197, 32, 252, 163, 12, 78, 37,
                74, 41, 150, 65, 82, 18, 180, 63, 79, 252, 212, 136, 252, 5, 190, 110, 59, 174, 64, 191, 69, 201, 207,
                62, 121, 97, 215, 180, 247, 142, 14, 100, 163, 89, 44, 243, 58, 168, 247, 157, 209, 115, 233, 64, 87,
                64, 234, 56, 212, 103, 57, 250, 110, 51, 73, 172, 73, 218, 7, 165, 26, 53, 118, 178, 209, 193, 66, 29,
                135, 234, 36, 63, 155, 147, 35, 140, 71, 117, 99, 41, 143, 37, 118, 1, 45, 218, 44, 57, 29, 135, 158,
                105, 212, 137, 151, 59, 214, 224, 35, 80, 255, 27, 175, 9, 247, 112, 253, 62, 190, 217, 163, 205, 223,
                228, 103, 94, 94, 2, 110, 228, 9, 33
            };
            byte[] dp =
            {
                41, 150, 25, 89, 187, 50, 253, 186, 9, 73, 45, 217, 240, 222, 109, 145, 224, 53, 245, 202, 185, 224,
                170, 210, 141, 147, 137, 18, 114, 73, 226, 220, 142, 81, 202, 255, 220, 5, 243, 139, 19, 128, 54, 166,
                239, 43, 120, 200, 131, 250, 154, 76, 241, 205, 56, 39, 85, 85, 239, 43, 88, 73, 22, 63, 47, 38, 216,
                53, 222, 194, 203, 116, 16, 254, 114, 37, 14, 10, 99, 239, 159, 70, 24, 107, 247, 5, 79, 33, 96, 98, 25,
                153, 224, 244, 117, 238, 213, 188, 194, 11, 185, 61, 10, 224, 187, 113, 201, 225, 40, 221, 35, 97, 26,
                48, 222, 95, 193, 154, 102, 125, 222, 61, 90, 22, 191, 229, 147, 173
            };
            byte[] dq =
            {
                99, 110, 74, 131, 51, 227, 71, 65, 35, 247, 77, 147, 32, 213, 249, 18, 59, 168, 84, 106, 244, 193, 218,
                103, 96, 45, 176, 236, 7, 158, 27, 4, 234, 188, 15, 194, 161, 120, 91, 247, 239, 189, 244, 57, 98, 124,
                38, 70, 126, 108, 246, 98, 190, 221, 104, 111, 240, 19, 52, 140, 128, 175, 223, 72, 69, 39, 78, 68, 172,
                86, 103, 104, 158, 148, 58, 231, 168, 80, 51, 71, 1, 232, 77, 251, 150, 106, 209, 125, 28, 233, 135, 58,
                122, 83, 127, 250, 103, 5, 152, 172, 120, 23, 3, 159, 199, 193, 125, 197, 26, 46, 161, 61, 230, 143,
                160, 31, 91, 56, 101, 135, 21, 180, 8, 80, 176, 29, 5, 221
            };
            byte[] exponent = { 1, 0, 1 };
            byte[] inverseQ =
            {
                211, 94, 119, 169, 124, 20, 144, 195, 119, 21, 4, 101, 88, 143, 6, 193, 197, 12, 104, 47, 209, 129, 81,
                51, 33, 141, 22, 144, 126, 165, 1, 88, 98, 85, 49, 38, 177, 137, 191, 186, 116, 6, 218, 48, 128, 21,
                195, 64, 200, 48, 161, 188, 181, 54, 13, 18, 49, 41, 200, 107, 135, 189, 40, 109, 226, 246, 81, 129,
                170, 194, 54, 160, 228, 215, 113, 10, 99, 205, 11, 147, 35, 175, 216, 127, 201, 211, 90, 237, 129, 166,
                100, 235, 225, 250, 217, 110, 115, 26, 114, 27, 183, 58, 29, 7, 105, 251, 37, 171, 10, 231, 173, 72,
                115, 244, 28, 172, 75, 97, 120, 46, 16, 154, 191, 145, 93, 237, 55, 166
            };
            byte[] mod =
            {
                209, 81, 37, 90, 132, 0, 217, 34, 181, 114, 193, 182, 254, 198, 63, 194, 91, 44, 59, 86, 227, 93, 28,
                126, 183, 226, 101, 34, 4, 186, 140, 179, 194, 121, 252, 130, 1, 198, 65, 191, 177, 37, 113, 35, 201,
                64, 131, 196, 92, 220, 13, 217, 119, 103, 77, 205, 176, 16, 136, 217, 241, 180, 224, 6, 80, 106, 94,
                154, 99, 227, 189, 91, 37, 229, 252, 50, 45, 174, 3, 16, 42, 57, 64, 238, 188, 107, 170, 182, 31, 166,
                171, 176, 204, 170, 137, 119, 48, 209, 249, 63, 209, 78, 234, 88, 187, 157, 156, 72, 212, 85, 247, 246,
                237, 171, 255, 129, 248, 61, 116, 98, 91, 29, 119, 130, 197, 80, 68, 190, 130, 150, 173, 163, 113, 113,
                71, 115, 208, 175, 53, 160, 144, 16, 149, 253, 230, 56, 16, 20, 12, 155, 106, 93, 181, 232, 76, 37, 158,
                117, 85, 130, 36, 255, 76, 219, 31, 22, 53, 184, 213, 114, 89, 176, 117, 39, 203, 217, 62, 116, 7, 242,
                63, 58, 128, 147, 158, 28, 130, 187, 227, 128, 1, 201, 107, 32, 214, 141, 170, 106, 247, 65, 102, 255,
                231, 191, 13, 84, 58, 4, 113, 164, 87, 67, 206, 64, 201, 36, 6, 187, 239, 132, 2, 16, 187, 109, 182,
                143, 237, 55, 126, 185, 189, 111, 108, 153, 174, 21, 255, 169, 45, 53, 151, 40, 214, 225, 86, 144, 202,
                209, 149, 211, 9, 94, 193, 94, 129, 23
            };
            byte[] p =
            {
                231, 163, 29, 139, 11, 6, 106, 184, 190, 126, 22, 155, 215, 249, 90, 10, 34, 95, 154, 93, 7, 64, 251,
                195, 22, 94, 102, 10, 243, 177, 67, 50, 117, 183, 91, 184, 246, 63, 156, 28, 134, 114, 81, 90, 219, 27,
                161, 146, 91, 181, 252, 152, 145, 10, 46, 124, 124, 100, 225, 22, 168, 217, 50, 45, 206, 245, 127, 187,
                146, 91, 43, 69, 218, 5, 185, 171, 38, 60, 224, 149, 16, 224, 180, 184, 81, 124, 250, 158, 120, 201,
                110, 124, 18, 239, 190, 159, 244, 106, 133, 65, 10, 39, 220, 202, 146, 1, 63, 74, 176, 51, 23, 151, 41,
                153, 147, 213, 179, 229, 50, 90, 90, 126, 162, 236, 84, 176, 13, 163
            };
            byte[] q =
            {
                231, 85, 13, 218, 199, 119, 199, 252, 63, 168, 10, 200, 68, 90, 15, 176, 17, 139, 130, 185, 117, 227,
                74, 222, 122, 201, 228, 20, 64, 195, 41, 94, 105, 189, 29, 229, 144, 169, 170, 254, 85, 146, 88, 5, 57,
                19, 216, 149, 3, 103, 46, 249, 38, 97, 74, 50, 150, 167, 52, 239, 90, 88, 230, 9, 111, 243, 134, 168,
                34, 50, 232, 160, 56, 174, 54, 93, 9, 47, 202, 67, 183, 102, 21, 165, 138, 168, 204, 187, 227, 152, 194,
                244, 183, 189, 183, 1, 23, 46, 249, 30, 248, 66, 235, 229, 61, 65, 112, 59, 176, 136, 52, 173, 204, 21,
                195, 77, 17, 195, 202, 40, 111, 67, 248, 39, 142, 13, 77, 253
            };

            bool areAllFieldsSerializable = typeof(RSAParameters)
                                                .GetField(nameof(RSAParameters.P))!
                                                .GetCustomAttribute<NonSerializedAttribute>() == null;

            if (!areAllFieldsSerializable)
            {
                _output.WriteLine("Some fields of RSAParameters struct are NOT serializable on this platform.");
            }

            if (areAllFieldsSerializable)
            {
                RSAParameters rsaParameters = JsonConvert.DeserializeObject<RSAParameters>(@"{
                    ""D"":""nrXEeOl2Ky3iHu0679sqJRg0ZZnwM32Tp6IFxNGJEX6Liysm+Gl3GP4cwbi8tCo1DaPc7HNY5Ol9AkyB1H+tDZyTyr+N1fSR1B6aJwHz22a6XUcavct1WTqOicg4p2Yr9dx//R+bkp1NRKhnoHi+14BG1imaLB6/JQ8muwUZY8Ug/KMMTiVKKZZBUhK0P0/81Ij8Bb5uO65Av0XJzz55Yde0944OZKNZLPM6qPed0XPpQFdA6jjUZzn6bjNJrEnaB6UaNXay0cFCHYfqJD+bkyOMR3VjKY8ldgEt2iw5HYeeadSJlzvW4CNQ/xuvCfdw/T6+2aPN3+RnXl4CbuQJIQ=="",
                    ""DP"":""KZYZWbsy/boJSS3Z8N5tkeA19cq54KrSjZOJEnJJ4tyOUcr/3AXzixOANqbvK3jIg/qaTPHNOCdVVe8rWEkWPy8m2DXewst0EP5yJQ4KY++fRhhr9wVPIWBiGZng9HXu1bzCC7k9CuC7ccnhKN0jYRow3l/BmmZ93j1aFr/lk60="",
                    ""DQ"":""Y25KgzPjR0Ej902TINX5EjuoVGr0wdpnYC2w7AeeGwTqvA/CoXhb9++99DlifCZGfmz2Yr7daG/wEzSMgK/fSEUnTkSsVmdonpQ656hQM0cB6E37lmrRfRzphzp6U3/6ZwWYrHgXA5/HwX3FGi6hPeaPoB9bOGWHFbQIULAdBd0="",
                    ""Exponent"":""AQAB"",
                    ""InverseQ"":""0153qXwUkMN3FQRlWI8GwcUMaC/RgVEzIY0WkH6lAVhiVTEmsYm/unQG2jCAFcNAyDChvLU2DRIxKchrh70obeL2UYGqwjag5NdxCmPNC5Mjr9h/ydNa7YGmZOvh+tlucxpyG7c6HQdp+yWrCuetSHP0HKxLYXguEJq/kV3tN6Y="",
                    ""Modulus"":""0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFw=="",
                    ""P"":""56MdiwsGari+fhab1/laCiJfml0HQPvDFl5mCvOxQzJ1t1u49j+cHIZyUVrbG6GSW7X8mJEKLnx8ZOEWqNkyLc71f7uSWytF2gW5qyY84JUQ4LS4UXz6nnjJbnwS776f9GqFQQon3MqSAT9KsDMXlymZk9Wz5TJaWn6i7FSwDaM="",
                    ""Q"":""51UN2sd3x/w/qArIRFoPsBGLgrl140reesnkFEDDKV5pvR3lkKmq/lWSWAU5E9iVA2cu+SZhSjKWpzTvWljmCW/zhqgiMuigOK42XQkvykO3ZhWliqjMu+OYwvS3vbcBFy75HvhC6+U9QXA7sIg0rcwVw00Rw8oob0P4J44NTf0=""
                }");
                Assert.Equal(exponent, rsaParameters.Exponent);
                Assert.Equal(mod, rsaParameters.Modulus);
                Assert.Equal(p, rsaParameters.P);
                Assert.Equal(q, rsaParameters.Q);
                Assert.Equal(d, rsaParameters.D);
                Assert.Equal(dp, rsaParameters.DP);
                Assert.Equal(dq, rsaParameters.DQ);
                Assert.Equal(inverseQ, rsaParameters.InverseQ);
            }
            else
            {
                RSAParameters rsaParameters = JsonConvert.DeserializeObject<RSAParameters>(@"{
                    ""Exponent"":""AQAB"",
                    ""Modulus"":""0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFw==""
                }");
                Assert.Equal(exponent, rsaParameters.Exponent);
                Assert.Equal(mod, rsaParameters.Modulus);
            }
        }

        [Fact(DisplayName = "Should JSON-serialize an EncryptionKeyParameters instance")]
        public void Should_JSON_Serialize_EncryptionKeyParameters()
        {
            byte[] d =
            {
                158, 181, 196, 120, 233, 118, 43, 45, 226, 30, 237, 58, 239, 219, 42, 37, 24, 52, 101, 153, 240, 51,
                125, 147, 167, 162, 5, 196, 209, 137, 17, 126, 139, 139, 43, 38, 248, 105, 119, 24, 254, 28, 193, 184,
                188, 180, 42, 53, 13, 163, 220, 236, 115, 88, 228, 233, 125, 2, 76, 129, 212, 127, 173, 13, 156, 147,
                202, 191, 141, 213, 244, 145, 212, 30, 154, 39, 1, 243, 219, 102, 186, 93, 71, 26, 189, 203, 117, 89,
                58, 142, 137, 200, 56, 167, 102, 43, 245, 220, 127, 253, 31, 155, 146, 157, 77, 68, 168, 103, 160, 120,
                190, 215, 128, 70, 214, 41, 154, 44, 30, 191, 37, 15, 38, 187, 5, 25, 99, 197, 32, 252, 163, 12, 78, 37,
                74, 41, 150, 65, 82, 18, 180, 63, 79, 252, 212, 136, 252, 5, 190, 110, 59, 174, 64, 191, 69, 201, 207,
                62, 121, 97, 215, 180, 247, 142, 14, 100, 163, 89, 44, 243, 58, 168, 247, 157, 209, 115, 233, 64, 87,
                64, 234, 56, 212, 103, 57, 250, 110, 51, 73, 172, 73, 218, 7, 165, 26, 53, 118, 178, 209, 193, 66, 29,
                135, 234, 36, 63, 155, 147, 35, 140, 71, 117, 99, 41, 143, 37, 118, 1, 45, 218, 44, 57, 29, 135, 158,
                105, 212, 137, 151, 59, 214, 224, 35, 80, 255, 27, 175, 9, 247, 112, 253, 62, 190, 217, 163, 205, 223,
                228, 103, 94, 94, 2, 110, 228, 9, 33
            };
            byte[] dp =
            {
                41, 150, 25, 89, 187, 50, 253, 186, 9, 73, 45, 217, 240, 222, 109, 145, 224, 53, 245, 202, 185, 224,
                170, 210, 141, 147, 137, 18, 114, 73, 226, 220, 142, 81, 202, 255, 220, 5, 243, 139, 19, 128, 54, 166,
                239, 43, 120, 200, 131, 250, 154, 76, 241, 205, 56, 39, 85, 85, 239, 43, 88, 73, 22, 63, 47, 38, 216,
                53, 222, 194, 203, 116, 16, 254, 114, 37, 14, 10, 99, 239, 159, 70, 24, 107, 247, 5, 79, 33, 96, 98, 25,
                153, 224, 244, 117, 238, 213, 188, 194, 11, 185, 61, 10, 224, 187, 113, 201, 225, 40, 221, 35, 97, 26,
                48, 222, 95, 193, 154, 102, 125, 222, 61, 90, 22, 191, 229, 147, 173
            };
            byte[] dq =
            {
                99, 110, 74, 131, 51, 227, 71, 65, 35, 247, 77, 147, 32, 213, 249, 18, 59, 168, 84, 106, 244, 193, 218,
                103, 96, 45, 176, 236, 7, 158, 27, 4, 234, 188, 15, 194, 161, 120, 91, 247, 239, 189, 244, 57, 98, 124,
                38, 70, 126, 108, 246, 98, 190, 221, 104, 111, 240, 19, 52, 140, 128, 175, 223, 72, 69, 39, 78, 68, 172,
                86, 103, 104, 158, 148, 58, 231, 168, 80, 51, 71, 1, 232, 77, 251, 150, 106, 209, 125, 28, 233, 135, 58,
                122, 83, 127, 250, 103, 5, 152, 172, 120, 23, 3, 159, 199, 193, 125, 197, 26, 46, 161, 61, 230, 143,
                160, 31, 91, 56, 101, 135, 21, 180, 8, 80, 176, 29, 5, 221
            };
            byte[] exponent = { 1, 0, 1 };
            byte[] inverseQ =
            {
                211, 94, 119, 169, 124, 20, 144, 195, 119, 21, 4, 101, 88, 143, 6, 193, 197, 12, 104, 47, 209, 129, 81,
                51, 33, 141, 22, 144, 126, 165, 1, 88, 98, 85, 49, 38, 177, 137, 191, 186, 116, 6, 218, 48, 128, 21,
                195, 64, 200, 48, 161, 188, 181, 54, 13, 18, 49, 41, 200, 107, 135, 189, 40, 109, 226, 246, 81, 129,
                170, 194, 54, 160, 228, 215, 113, 10, 99, 205, 11, 147, 35, 175, 216, 127, 201, 211, 90, 237, 129, 166,
                100, 235, 225, 250, 217, 110, 115, 26, 114, 27, 183, 58, 29, 7, 105, 251, 37, 171, 10, 231, 173, 72,
                115, 244, 28, 172, 75, 97, 120, 46, 16, 154, 191, 145, 93, 237, 55, 166
            };
            byte[] mod =
            {
                209, 81, 37, 90, 132, 0, 217, 34, 181, 114, 193, 182, 254, 198, 63, 194, 91, 44, 59, 86, 227, 93, 28,
                126, 183, 226, 101, 34, 4, 186, 140, 179, 194, 121, 252, 130, 1, 198, 65, 191, 177, 37, 113, 35, 201,
                64, 131, 196, 92, 220, 13, 217, 119, 103, 77, 205, 176, 16, 136, 217, 241, 180, 224, 6, 80, 106, 94,
                154, 99, 227, 189, 91, 37, 229, 252, 50, 45, 174, 3, 16, 42, 57, 64, 238, 188, 107, 170, 182, 31, 166,
                171, 176, 204, 170, 137, 119, 48, 209, 249, 63, 209, 78, 234, 88, 187, 157, 156, 72, 212, 85, 247, 246,
                237, 171, 255, 129, 248, 61, 116, 98, 91, 29, 119, 130, 197, 80, 68, 190, 130, 150, 173, 163, 113, 113,
                71, 115, 208, 175, 53, 160, 144, 16, 149, 253, 230, 56, 16, 20, 12, 155, 106, 93, 181, 232, 76, 37, 158,
                117, 85, 130, 36, 255, 76, 219, 31, 22, 53, 184, 213, 114, 89, 176, 117, 39, 203, 217, 62, 116, 7, 242,
                63, 58, 128, 147, 158, 28, 130, 187, 227, 128, 1, 201, 107, 32, 214, 141, 170, 106, 247, 65, 102, 255,
                231, 191, 13, 84, 58, 4, 113, 164, 87, 67, 206, 64, 201, 36, 6, 187, 239, 132, 2, 16, 187, 109, 182,
                143, 237, 55, 126, 185, 189, 111, 108, 153, 174, 21, 255, 169, 45, 53, 151, 40, 214, 225, 86, 144, 202,
                209, 149, 211, 9, 94, 193, 94, 129, 23
            };
            byte[] p =
            {
                231, 163, 29, 139, 11, 6, 106, 184, 190, 126, 22, 155, 215, 249, 90, 10, 34, 95, 154, 93, 7, 64, 251,
                195, 22, 94, 102, 10, 243, 177, 67, 50, 117, 183, 91, 184, 246, 63, 156, 28, 134, 114, 81, 90, 219, 27,
                161, 146, 91, 181, 252, 152, 145, 10, 46, 124, 124, 100, 225, 22, 168, 217, 50, 45, 206, 245, 127, 187,
                146, 91, 43, 69, 218, 5, 185, 171, 38, 60, 224, 149, 16, 224, 180, 184, 81, 124, 250, 158, 120, 201,
                110, 124, 18, 239, 190, 159, 244, 106, 133, 65, 10, 39, 220, 202, 146, 1, 63, 74, 176, 51, 23, 151, 41,
                153, 147, 213, 179, 229, 50, 90, 90, 126, 162, 236, 84, 176, 13, 163
            };
            byte[] q =
            {
                231, 85, 13, 218, 199, 119, 199, 252, 63, 168, 10, 200, 68, 90, 15, 176, 17, 139, 130, 185, 117, 227,
                74, 222, 122, 201, 228, 20, 64, 195, 41, 94, 105, 189, 29, 229, 144, 169, 170, 254, 85, 146, 88, 5, 57,
                19, 216, 149, 3, 103, 46, 249, 38, 97, 74, 50, 150, 167, 52, 239, 90, 88, 230, 9, 111, 243, 134, 168,
                34, 50, 232, 160, 56, 174, 54, 93, 9, 47, 202, 67, 183, 102, 21, 165, 138, 168, 204, 187, 227, 152, 194,
                244, 183, 189, 183, 1, 23, 46, 249, 30, 248, 66, 235, 229, 61, 65, 112, 59, 176, 136, 52, 173, 204, 21,
                195, 77, 17, 195, 202, 40, 111, 67, 248, 39, 142, 13, 77, 253
            };

            EncryptionKeyParameters keyParameters = new EncryptionKeyParameters
            {
                E = exponent,
                M = mod,
                P = p,
                Q = q,
                D = d,
                DP = dp,
                DQ = dq,
                IQ = inverseQ,
            };

            string json = JsonConvert.SerializeObject(keyParameters);
            _output.WriteLine("JSON value is:\n" + json);

            Assert.True(JToken.DeepEquals(
                JToken.Parse(@"{
                    ""E"":""AQAB"",
                    ""M"":""0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFw=="",
                    ""P"":""56MdiwsGari+fhab1/laCiJfml0HQPvDFl5mCvOxQzJ1t1u49j+cHIZyUVrbG6GSW7X8mJEKLnx8ZOEWqNkyLc71f7uSWytF2gW5qyY84JUQ4LS4UXz6nnjJbnwS776f9GqFQQon3MqSAT9KsDMXlymZk9Wz5TJaWn6i7FSwDaM="",
                    ""Q"":""51UN2sd3x/w/qArIRFoPsBGLgrl140reesnkFEDDKV5pvR3lkKmq/lWSWAU5E9iVA2cu+SZhSjKWpzTvWljmCW/zhqgiMuigOK42XQkvykO3ZhWliqjMu+OYwvS3vbcBFy75HvhC6+U9QXA7sIg0rcwVw00Rw8oob0P4J44NTf0="",
                    ""D"":""nrXEeOl2Ky3iHu0679sqJRg0ZZnwM32Tp6IFxNGJEX6Liysm+Gl3GP4cwbi8tCo1DaPc7HNY5Ol9AkyB1H+tDZyTyr+N1fSR1B6aJwHz22a6XUcavct1WTqOicg4p2Yr9dx//R+bkp1NRKhnoHi+14BG1imaLB6/JQ8muwUZY8Ug/KMMTiVKKZZBUhK0P0/81Ij8Bb5uO65Av0XJzz55Yde0944OZKNZLPM6qPed0XPpQFdA6jjUZzn6bjNJrEnaB6UaNXay0cFCHYfqJD+bkyOMR3VjKY8ldgEt2iw5HYeeadSJlzvW4CNQ/xuvCfdw/T6+2aPN3+RnXl4CbuQJIQ=="",
                    ""DP"":""KZYZWbsy/boJSS3Z8N5tkeA19cq54KrSjZOJEnJJ4tyOUcr/3AXzixOANqbvK3jIg/qaTPHNOCdVVe8rWEkWPy8m2DXewst0EP5yJQ4KY++fRhhr9wVPIWBiGZng9HXu1bzCC7k9CuC7ccnhKN0jYRow3l/BmmZ93j1aFr/lk60="",
                    ""DQ"":""Y25KgzPjR0Ej902TINX5EjuoVGr0wdpnYC2w7AeeGwTqvA/CoXhb9++99DlifCZGfmz2Yr7daG/wEzSMgK/fSEUnTkSsVmdonpQ656hQM0cB6E37lmrRfRzphzp6U3/6ZwWYrHgXA5/HwX3FGi6hPeaPoB9bOGWHFbQIULAdBd0="",
                    ""IQ"":""0153qXwUkMN3FQRlWI8GwcUMaC/RgVEzIY0WkH6lAVhiVTEmsYm/unQG2jCAFcNAyDChvLU2DRIxKchrh70obeL2UYGqwjag5NdxCmPNC5Mjr9h/ydNa7YGmZOvh+tlucxpyG7c6HQdp+yWrCuetSHP0HKxLYXguEJq/kV3tN6Y=""
                }"),
                JToken.Parse(json)
            ));
        }

        [Fact(DisplayName = "Should deserialize an EncryptionKeyParameters instance from JSON")]
        public void Should_Deserialize_EncryptionKeyParameters_From_JSON()
        {
            byte[] d =
            {
                158, 181, 196, 120, 233, 118, 43, 45, 226, 30, 237, 58, 239, 219, 42, 37, 24, 52, 101, 153, 240, 51,
                125, 147, 167, 162, 5, 196, 209, 137, 17, 126, 139, 139, 43, 38, 248, 105, 119, 24, 254, 28, 193, 184,
                188, 180, 42, 53, 13, 163, 220, 236, 115, 88, 228, 233, 125, 2, 76, 129, 212, 127, 173, 13, 156, 147,
                202, 191, 141, 213, 244, 145, 212, 30, 154, 39, 1, 243, 219, 102, 186, 93, 71, 26, 189, 203, 117, 89,
                58, 142, 137, 200, 56, 167, 102, 43, 245, 220, 127, 253, 31, 155, 146, 157, 77, 68, 168, 103, 160, 120,
                190, 215, 128, 70, 214, 41, 154, 44, 30, 191, 37, 15, 38, 187, 5, 25, 99, 197, 32, 252, 163, 12, 78, 37,
                74, 41, 150, 65, 82, 18, 180, 63, 79, 252, 212, 136, 252, 5, 190, 110, 59, 174, 64, 191, 69, 201, 207,
                62, 121, 97, 215, 180, 247, 142, 14, 100, 163, 89, 44, 243, 58, 168, 247, 157, 209, 115, 233, 64, 87,
                64, 234, 56, 212, 103, 57, 250, 110, 51, 73, 172, 73, 218, 7, 165, 26, 53, 118, 178, 209, 193, 66, 29,
                135, 234, 36, 63, 155, 147, 35, 140, 71, 117, 99, 41, 143, 37, 118, 1, 45, 218, 44, 57, 29, 135, 158,
                105, 212, 137, 151, 59, 214, 224, 35, 80, 255, 27, 175, 9, 247, 112, 253, 62, 190, 217, 163, 205, 223,
                228, 103, 94, 94, 2, 110, 228, 9, 33
            };
            byte[] dp =
            {
                41, 150, 25, 89, 187, 50, 253, 186, 9, 73, 45, 217, 240, 222, 109, 145, 224, 53, 245, 202, 185, 224,
                170, 210, 141, 147, 137, 18, 114, 73, 226, 220, 142, 81, 202, 255, 220, 5, 243, 139, 19, 128, 54, 166,
                239, 43, 120, 200, 131, 250, 154, 76, 241, 205, 56, 39, 85, 85, 239, 43, 88, 73, 22, 63, 47, 38, 216,
                53, 222, 194, 203, 116, 16, 254, 114, 37, 14, 10, 99, 239, 159, 70, 24, 107, 247, 5, 79, 33, 96, 98, 25,
                153, 224, 244, 117, 238, 213, 188, 194, 11, 185, 61, 10, 224, 187, 113, 201, 225, 40, 221, 35, 97, 26,
                48, 222, 95, 193, 154, 102, 125, 222, 61, 90, 22, 191, 229, 147, 173
            };
            byte[] dq =
            {
                99, 110, 74, 131, 51, 227, 71, 65, 35, 247, 77, 147, 32, 213, 249, 18, 59, 168, 84, 106, 244, 193, 218,
                103, 96, 45, 176, 236, 7, 158, 27, 4, 234, 188, 15, 194, 161, 120, 91, 247, 239, 189, 244, 57, 98, 124,
                38, 70, 126, 108, 246, 98, 190, 221, 104, 111, 240, 19, 52, 140, 128, 175, 223, 72, 69, 39, 78, 68, 172,
                86, 103, 104, 158, 148, 58, 231, 168, 80, 51, 71, 1, 232, 77, 251, 150, 106, 209, 125, 28, 233, 135, 58,
                122, 83, 127, 250, 103, 5, 152, 172, 120, 23, 3, 159, 199, 193, 125, 197, 26, 46, 161, 61, 230, 143,
                160, 31, 91, 56, 101, 135, 21, 180, 8, 80, 176, 29, 5, 221
            };
            byte[] exponent = { 1, 0, 1 };
            byte[] inverseQ =
            {
                211, 94, 119, 169, 124, 20, 144, 195, 119, 21, 4, 101, 88, 143, 6, 193, 197, 12, 104, 47, 209, 129, 81,
                51, 33, 141, 22, 144, 126, 165, 1, 88, 98, 85, 49, 38, 177, 137, 191, 186, 116, 6, 218, 48, 128, 21,
                195, 64, 200, 48, 161, 188, 181, 54, 13, 18, 49, 41, 200, 107, 135, 189, 40, 109, 226, 246, 81, 129,
                170, 194, 54, 160, 228, 215, 113, 10, 99, 205, 11, 147, 35, 175, 216, 127, 201, 211, 90, 237, 129, 166,
                100, 235, 225, 250, 217, 110, 115, 26, 114, 27, 183, 58, 29, 7, 105, 251, 37, 171, 10, 231, 173, 72,
                115, 244, 28, 172, 75, 97, 120, 46, 16, 154, 191, 145, 93, 237, 55, 166
            };
            byte[] mod =
            {
                209, 81, 37, 90, 132, 0, 217, 34, 181, 114, 193, 182, 254, 198, 63, 194, 91, 44, 59, 86, 227, 93, 28,
                126, 183, 226, 101, 34, 4, 186, 140, 179, 194, 121, 252, 130, 1, 198, 65, 191, 177, 37, 113, 35, 201,
                64, 131, 196, 92, 220, 13, 217, 119, 103, 77, 205, 176, 16, 136, 217, 241, 180, 224, 6, 80, 106, 94,
                154, 99, 227, 189, 91, 37, 229, 252, 50, 45, 174, 3, 16, 42, 57, 64, 238, 188, 107, 170, 182, 31, 166,
                171, 176, 204, 170, 137, 119, 48, 209, 249, 63, 209, 78, 234, 88, 187, 157, 156, 72, 212, 85, 247, 246,
                237, 171, 255, 129, 248, 61, 116, 98, 91, 29, 119, 130, 197, 80, 68, 190, 130, 150, 173, 163, 113, 113,
                71, 115, 208, 175, 53, 160, 144, 16, 149, 253, 230, 56, 16, 20, 12, 155, 106, 93, 181, 232, 76, 37, 158,
                117, 85, 130, 36, 255, 76, 219, 31, 22, 53, 184, 213, 114, 89, 176, 117, 39, 203, 217, 62, 116, 7, 242,
                63, 58, 128, 147, 158, 28, 130, 187, 227, 128, 1, 201, 107, 32, 214, 141, 170, 106, 247, 65, 102, 255,
                231, 191, 13, 84, 58, 4, 113, 164, 87, 67, 206, 64, 201, 36, 6, 187, 239, 132, 2, 16, 187, 109, 182,
                143, 237, 55, 126, 185, 189, 111, 108, 153, 174, 21, 255, 169, 45, 53, 151, 40, 214, 225, 86, 144, 202,
                209, 149, 211, 9, 94, 193, 94, 129, 23
            };
            byte[] p =
            {
                231, 163, 29, 139, 11, 6, 106, 184, 190, 126, 22, 155, 215, 249, 90, 10, 34, 95, 154, 93, 7, 64, 251,
                195, 22, 94, 102, 10, 243, 177, 67, 50, 117, 183, 91, 184, 246, 63, 156, 28, 134, 114, 81, 90, 219, 27,
                161, 146, 91, 181, 252, 152, 145, 10, 46, 124, 124, 100, 225, 22, 168, 217, 50, 45, 206, 245, 127, 187,
                146, 91, 43, 69, 218, 5, 185, 171, 38, 60, 224, 149, 16, 224, 180, 184, 81, 124, 250, 158, 120, 201,
                110, 124, 18, 239, 190, 159, 244, 106, 133, 65, 10, 39, 220, 202, 146, 1, 63, 74, 176, 51, 23, 151, 41,
                153, 147, 213, 179, 229, 50, 90, 90, 126, 162, 236, 84, 176, 13, 163
            };
            byte[] q =
            {
                231, 85, 13, 218, 199, 119, 199, 252, 63, 168, 10, 200, 68, 90, 15, 176, 17, 139, 130, 185, 117, 227,
                74, 222, 122, 201, 228, 20, 64, 195, 41, 94, 105, 189, 29, 229, 144, 169, 170, 254, 85, 146, 88, 5, 57,
                19, 216, 149, 3, 103, 46, 249, 38, 97, 74, 50, 150, 167, 52, 239, 90, 88, 230, 9, 111, 243, 134, 168,
                34, 50, 232, 160, 56, 174, 54, 93, 9, 47, 202, 67, 183, 102, 21, 165, 138, 168, 204, 187, 227, 152, 194,
                244, 183, 189, 183, 1, 23, 46, 249, 30, 248, 66, 235, 229, 61, 65, 112, 59, 176, 136, 52, 173, 204, 21,
                195, 77, 17, 195, 202, 40, 111, 67, 248, 39, 142, 13, 77, 253
            };

            EncryptionKeyParameters keyParameters = JsonConvert.DeserializeObject<EncryptionKeyParameters>(@"{
                    ""E"":""AQAB"",
                    ""M"":""0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFw=="",
                    ""P"":""56MdiwsGari+fhab1/laCiJfml0HQPvDFl5mCvOxQzJ1t1u49j+cHIZyUVrbG6GSW7X8mJEKLnx8ZOEWqNkyLc71f7uSWytF2gW5qyY84JUQ4LS4UXz6nnjJbnwS776f9GqFQQon3MqSAT9KsDMXlymZk9Wz5TJaWn6i7FSwDaM="",
                    ""Q"":""51UN2sd3x/w/qArIRFoPsBGLgrl140reesnkFEDDKV5pvR3lkKmq/lWSWAU5E9iVA2cu+SZhSjKWpzTvWljmCW/zhqgiMuigOK42XQkvykO3ZhWliqjMu+OYwvS3vbcBFy75HvhC6+U9QXA7sIg0rcwVw00Rw8oob0P4J44NTf0="",
                    ""D"":""nrXEeOl2Ky3iHu0679sqJRg0ZZnwM32Tp6IFxNGJEX6Liysm+Gl3GP4cwbi8tCo1DaPc7HNY5Ol9AkyB1H+tDZyTyr+N1fSR1B6aJwHz22a6XUcavct1WTqOicg4p2Yr9dx//R+bkp1NRKhnoHi+14BG1imaLB6/JQ8muwUZY8Ug/KMMTiVKKZZBUhK0P0/81Ij8Bb5uO65Av0XJzz55Yde0944OZKNZLPM6qPed0XPpQFdA6jjUZzn6bjNJrEnaB6UaNXay0cFCHYfqJD+bkyOMR3VjKY8ldgEt2iw5HYeeadSJlzvW4CNQ/xuvCfdw/T6+2aPN3+RnXl4CbuQJIQ=="",
                    ""DP"":""KZYZWbsy/boJSS3Z8N5tkeA19cq54KrSjZOJEnJJ4tyOUcr/3AXzixOANqbvK3jIg/qaTPHNOCdVVe8rWEkWPy8m2DXewst0EP5yJQ4KY++fRhhr9wVPIWBiGZng9HXu1bzCC7k9CuC7ccnhKN0jYRow3l/BmmZ93j1aFr/lk60="",
                    ""DQ"":""Y25KgzPjR0Ej902TINX5EjuoVGr0wdpnYC2w7AeeGwTqvA/CoXhb9++99DlifCZGfmz2Yr7daG/wEzSMgK/fSEUnTkSsVmdonpQ656hQM0cB6E37lmrRfRzphzp6U3/6ZwWYrHgXA5/HwX3FGi6hPeaPoB9bOGWHFbQIULAdBd0="",
                    ""IQ"":""0153qXwUkMN3FQRlWI8GwcUMaC/RgVEzIY0WkH6lAVhiVTEmsYm/unQG2jCAFcNAyDChvLU2DRIxKchrh70obeL2UYGqwjag5NdxCmPNC5Mjr9h/ydNa7YGmZOvh+tlucxpyG7c6HQdp+yWrCuetSHP0HKxLYXguEJq/kV3tN6Y=""
            }");

            Assert.Equal(exponent, keyParameters.E);
            Assert.Equal(mod, keyParameters.M);
            Assert.Equal(p, keyParameters.P);
            Assert.Equal(q, keyParameters.Q);
            Assert.Equal(d, keyParameters.D);
            Assert.Equal(dp, keyParameters.DP);
            Assert.Equal(dq, keyParameters.DQ);
            Assert.Equal(inverseQ, keyParameters.IQ);
        }

        [Fact(DisplayName = "Should convert a RSAParameters instance to an EncryptionKeyParameters instance")]
        public void Should_Cast_RSAParameters_To_EncryptionKeyParameters()
        {
            byte[] d =
            {
                158, 181, 196, 120, 233, 118, 43, 45, 226, 30, 237, 58, 239, 219, 42, 37, 24, 52, 101, 153, 240, 51,
                125, 147, 167, 162, 5, 196, 209, 137, 17, 126, 139, 139, 43, 38, 248, 105, 119, 24, 254, 28, 193, 184,
                188, 180, 42, 53, 13, 163, 220, 236, 115, 88, 228, 233, 125, 2, 76, 129, 212, 127, 173, 13, 156, 147,
                202, 191, 141, 213, 244, 145, 212, 30, 154, 39, 1, 243, 219, 102, 186, 93, 71, 26, 189, 203, 117, 89,
                58, 142, 137, 200, 56, 167, 102, 43, 245, 220, 127, 253, 31, 155, 146, 157, 77, 68, 168, 103, 160, 120,
                190, 215, 128, 70, 214, 41, 154, 44, 30, 191, 37, 15, 38, 187, 5, 25, 99, 197, 32, 252, 163, 12, 78, 37,
                74, 41, 150, 65, 82, 18, 180, 63, 79, 252, 212, 136, 252, 5, 190, 110, 59, 174, 64, 191, 69, 201, 207,
                62, 121, 97, 215, 180, 247, 142, 14, 100, 163, 89, 44, 243, 58, 168, 247, 157, 209, 115, 233, 64, 87,
                64, 234, 56, 212, 103, 57, 250, 110, 51, 73, 172, 73, 218, 7, 165, 26, 53, 118, 178, 209, 193, 66, 29,
                135, 234, 36, 63, 155, 147, 35, 140, 71, 117, 99, 41, 143, 37, 118, 1, 45, 218, 44, 57, 29, 135, 158,
                105, 212, 137, 151, 59, 214, 224, 35, 80, 255, 27, 175, 9, 247, 112, 253, 62, 190, 217, 163, 205, 223,
                228, 103, 94, 94, 2, 110, 228, 9, 33
            };
            byte[] dp =
            {
                41, 150, 25, 89, 187, 50, 253, 186, 9, 73, 45, 217, 240, 222, 109, 145, 224, 53, 245, 202, 185, 224,
                170, 210, 141, 147, 137, 18, 114, 73, 226, 220, 142, 81, 202, 255, 220, 5, 243, 139, 19, 128, 54, 166,
                239, 43, 120, 200, 131, 250, 154, 76, 241, 205, 56, 39, 85, 85, 239, 43, 88, 73, 22, 63, 47, 38, 216,
                53, 222, 194, 203, 116, 16, 254, 114, 37, 14, 10, 99, 239, 159, 70, 24, 107, 247, 5, 79, 33, 96, 98, 25,
                153, 224, 244, 117, 238, 213, 188, 194, 11, 185, 61, 10, 224, 187, 113, 201, 225, 40, 221, 35, 97, 26,
                48, 222, 95, 193, 154, 102, 125, 222, 61, 90, 22, 191, 229, 147, 173
            };
            byte[] dq =
            {
                99, 110, 74, 131, 51, 227, 71, 65, 35, 247, 77, 147, 32, 213, 249, 18, 59, 168, 84, 106, 244, 193, 218,
                103, 96, 45, 176, 236, 7, 158, 27, 4, 234, 188, 15, 194, 161, 120, 91, 247, 239, 189, 244, 57, 98, 124,
                38, 70, 126, 108, 246, 98, 190, 221, 104, 111, 240, 19, 52, 140, 128, 175, 223, 72, 69, 39, 78, 68, 172,
                86, 103, 104, 158, 148, 58, 231, 168, 80, 51, 71, 1, 232, 77, 251, 150, 106, 209, 125, 28, 233, 135, 58,
                122, 83, 127, 250, 103, 5, 152, 172, 120, 23, 3, 159, 199, 193, 125, 197, 26, 46, 161, 61, 230, 143,
                160, 31, 91, 56, 101, 135, 21, 180, 8, 80, 176, 29, 5, 221
            };
            byte[] exponent = { 1, 0, 1 };
            byte[] inverseQ =
            {
                211, 94, 119, 169, 124, 20, 144, 195, 119, 21, 4, 101, 88, 143, 6, 193, 197, 12, 104, 47, 209, 129, 81,
                51, 33, 141, 22, 144, 126, 165, 1, 88, 98, 85, 49, 38, 177, 137, 191, 186, 116, 6, 218, 48, 128, 21,
                195, 64, 200, 48, 161, 188, 181, 54, 13, 18, 49, 41, 200, 107, 135, 189, 40, 109, 226, 246, 81, 129,
                170, 194, 54, 160, 228, 215, 113, 10, 99, 205, 11, 147, 35, 175, 216, 127, 201, 211, 90, 237, 129, 166,
                100, 235, 225, 250, 217, 110, 115, 26, 114, 27, 183, 58, 29, 7, 105, 251, 37, 171, 10, 231, 173, 72,
                115, 244, 28, 172, 75, 97, 120, 46, 16, 154, 191, 145, 93, 237, 55, 166
            };
            byte[] mod =
            {
                209, 81, 37, 90, 132, 0, 217, 34, 181, 114, 193, 182, 254, 198, 63, 194, 91, 44, 59, 86, 227, 93, 28,
                126, 183, 226, 101, 34, 4, 186, 140, 179, 194, 121, 252, 130, 1, 198, 65, 191, 177, 37, 113, 35, 201,
                64, 131, 196, 92, 220, 13, 217, 119, 103, 77, 205, 176, 16, 136, 217, 241, 180, 224, 6, 80, 106, 94,
                154, 99, 227, 189, 91, 37, 229, 252, 50, 45, 174, 3, 16, 42, 57, 64, 238, 188, 107, 170, 182, 31, 166,
                171, 176, 204, 170, 137, 119, 48, 209, 249, 63, 209, 78, 234, 88, 187, 157, 156, 72, 212, 85, 247, 246,
                237, 171, 255, 129, 248, 61, 116, 98, 91, 29, 119, 130, 197, 80, 68, 190, 130, 150, 173, 163, 113, 113,
                71, 115, 208, 175, 53, 160, 144, 16, 149, 253, 230, 56, 16, 20, 12, 155, 106, 93, 181, 232, 76, 37, 158,
                117, 85, 130, 36, 255, 76, 219, 31, 22, 53, 184, 213, 114, 89, 176, 117, 39, 203, 217, 62, 116, 7, 242,
                63, 58, 128, 147, 158, 28, 130, 187, 227, 128, 1, 201, 107, 32, 214, 141, 170, 106, 247, 65, 102, 255,
                231, 191, 13, 84, 58, 4, 113, 164, 87, 67, 206, 64, 201, 36, 6, 187, 239, 132, 2, 16, 187, 109, 182,
                143, 237, 55, 126, 185, 189, 111, 108, 153, 174, 21, 255, 169, 45, 53, 151, 40, 214, 225, 86, 144, 202,
                209, 149, 211, 9, 94, 193, 94, 129, 23
            };
            byte[] p =
            {
                231, 163, 29, 139, 11, 6, 106, 184, 190, 126, 22, 155, 215, 249, 90, 10, 34, 95, 154, 93, 7, 64, 251,
                195, 22, 94, 102, 10, 243, 177, 67, 50, 117, 183, 91, 184, 246, 63, 156, 28, 134, 114, 81, 90, 219, 27,
                161, 146, 91, 181, 252, 152, 145, 10, 46, 124, 124, 100, 225, 22, 168, 217, 50, 45, 206, 245, 127, 187,
                146, 91, 43, 69, 218, 5, 185, 171, 38, 60, 224, 149, 16, 224, 180, 184, 81, 124, 250, 158, 120, 201,
                110, 124, 18, 239, 190, 159, 244, 106, 133, 65, 10, 39, 220, 202, 146, 1, 63, 74, 176, 51, 23, 151, 41,
                153, 147, 213, 179, 229, 50, 90, 90, 126, 162, 236, 84, 176, 13, 163
            };
            byte[] q =
            {
                231, 85, 13, 218, 199, 119, 199, 252, 63, 168, 10, 200, 68, 90, 15, 176, 17, 139, 130, 185, 117, 227,
                74, 222, 122, 201, 228, 20, 64, 195, 41, 94, 105, 189, 29, 229, 144, 169, 170, 254, 85, 146, 88, 5, 57,
                19, 216, 149, 3, 103, 46, 249, 38, 97, 74, 50, 150, 167, 52, 239, 90, 88, 230, 9, 111, 243, 134, 168,
                34, 50, 232, 160, 56, 174, 54, 93, 9, 47, 202, 67, 183, 102, 21, 165, 138, 168, 204, 187, 227, 152, 194,
                244, 183, 189, 183, 1, 23, 46, 249, 30, 248, 66, 235, 229, 61, 65, 112, 59, 176, 136, 52, 173, 204, 21,
                195, 77, 17, 195, 202, 40, 111, 67, 248, 39, 142, 13, 77, 253
            };

            RSAParameters rsaParameters = new RSAParameters
            {
                Exponent = exponent,
                Modulus = mod,
                P = p,
                Q = q,
                D = d,
                DP = dp,
                DQ = dq,
                InverseQ = inverseQ,
            };

            EncryptionKeyParameters keyParameters = (EncryptionKeyParameters) rsaParameters;

            Assert.Equal(exponent, keyParameters.E);
            Assert.Equal(mod, keyParameters.M);
            Assert.Equal(p, keyParameters.P);
            Assert.Equal(q, keyParameters.Q);
            Assert.Equal(d, keyParameters.D);
            Assert.Equal(dp, keyParameters.DP);
            Assert.Equal(dq, keyParameters.DQ);
            Assert.Equal(inverseQ, keyParameters.IQ);
        }

        [Fact(DisplayName = "Should convert an EncryptionKeyParameters instance to a RSAParameters instance")]
        public void Should_Cast_EncryptionKeyParameters_To_RSAParameters()
        {
            byte[] d =
            {
                158, 181, 196, 120, 233, 118, 43, 45, 226, 30, 237, 58, 239, 219, 42, 37, 24, 52, 101, 153, 240, 51,
                125, 147, 167, 162, 5, 196, 209, 137, 17, 126, 139, 139, 43, 38, 248, 105, 119, 24, 254, 28, 193, 184,
                188, 180, 42, 53, 13, 163, 220, 236, 115, 88, 228, 233, 125, 2, 76, 129, 212, 127, 173, 13, 156, 147,
                202, 191, 141, 213, 244, 145, 212, 30, 154, 39, 1, 243, 219, 102, 186, 93, 71, 26, 189, 203, 117, 89,
                58, 142, 137, 200, 56, 167, 102, 43, 245, 220, 127, 253, 31, 155, 146, 157, 77, 68, 168, 103, 160, 120,
                190, 215, 128, 70, 214, 41, 154, 44, 30, 191, 37, 15, 38, 187, 5, 25, 99, 197, 32, 252, 163, 12, 78, 37,
                74, 41, 150, 65, 82, 18, 180, 63, 79, 252, 212, 136, 252, 5, 190, 110, 59, 174, 64, 191, 69, 201, 207,
                62, 121, 97, 215, 180, 247, 142, 14, 100, 163, 89, 44, 243, 58, 168, 247, 157, 209, 115, 233, 64, 87,
                64, 234, 56, 212, 103, 57, 250, 110, 51, 73, 172, 73, 218, 7, 165, 26, 53, 118, 178, 209, 193, 66, 29,
                135, 234, 36, 63, 155, 147, 35, 140, 71, 117, 99, 41, 143, 37, 118, 1, 45, 218, 44, 57, 29, 135, 158,
                105, 212, 137, 151, 59, 214, 224, 35, 80, 255, 27, 175, 9, 247, 112, 253, 62, 190, 217, 163, 205, 223,
                228, 103, 94, 94, 2, 110, 228, 9, 33
            };
            byte[] dp =
            {
                41, 150, 25, 89, 187, 50, 253, 186, 9, 73, 45, 217, 240, 222, 109, 145, 224, 53, 245, 202, 185, 224,
                170, 210, 141, 147, 137, 18, 114, 73, 226, 220, 142, 81, 202, 255, 220, 5, 243, 139, 19, 128, 54, 166,
                239, 43, 120, 200, 131, 250, 154, 76, 241, 205, 56, 39, 85, 85, 239, 43, 88, 73, 22, 63, 47, 38, 216,
                53, 222, 194, 203, 116, 16, 254, 114, 37, 14, 10, 99, 239, 159, 70, 24, 107, 247, 5, 79, 33, 96, 98, 25,
                153, 224, 244, 117, 238, 213, 188, 194, 11, 185, 61, 10, 224, 187, 113, 201, 225, 40, 221, 35, 97, 26,
                48, 222, 95, 193, 154, 102, 125, 222, 61, 90, 22, 191, 229, 147, 173
            };
            byte[] dq =
            {
                99, 110, 74, 131, 51, 227, 71, 65, 35, 247, 77, 147, 32, 213, 249, 18, 59, 168, 84, 106, 244, 193, 218,
                103, 96, 45, 176, 236, 7, 158, 27, 4, 234, 188, 15, 194, 161, 120, 91, 247, 239, 189, 244, 57, 98, 124,
                38, 70, 126, 108, 246, 98, 190, 221, 104, 111, 240, 19, 52, 140, 128, 175, 223, 72, 69, 39, 78, 68, 172,
                86, 103, 104, 158, 148, 58, 231, 168, 80, 51, 71, 1, 232, 77, 251, 150, 106, 209, 125, 28, 233, 135, 58,
                122, 83, 127, 250, 103, 5, 152, 172, 120, 23, 3, 159, 199, 193, 125, 197, 26, 46, 161, 61, 230, 143,
                160, 31, 91, 56, 101, 135, 21, 180, 8, 80, 176, 29, 5, 221
            };
            byte[] exponent = { 1, 0, 1 };
            byte[] inverseQ =
            {
                211, 94, 119, 169, 124, 20, 144, 195, 119, 21, 4, 101, 88, 143, 6, 193, 197, 12, 104, 47, 209, 129, 81,
                51, 33, 141, 22, 144, 126, 165, 1, 88, 98, 85, 49, 38, 177, 137, 191, 186, 116, 6, 218, 48, 128, 21,
                195, 64, 200, 48, 161, 188, 181, 54, 13, 18, 49, 41, 200, 107, 135, 189, 40, 109, 226, 246, 81, 129,
                170, 194, 54, 160, 228, 215, 113, 10, 99, 205, 11, 147, 35, 175, 216, 127, 201, 211, 90, 237, 129, 166,
                100, 235, 225, 250, 217, 110, 115, 26, 114, 27, 183, 58, 29, 7, 105, 251, 37, 171, 10, 231, 173, 72,
                115, 244, 28, 172, 75, 97, 120, 46, 16, 154, 191, 145, 93, 237, 55, 166
            };
            byte[] mod =
            {
                209, 81, 37, 90, 132, 0, 217, 34, 181, 114, 193, 182, 254, 198, 63, 194, 91, 44, 59, 86, 227, 93, 28,
                126, 183, 226, 101, 34, 4, 186, 140, 179, 194, 121, 252, 130, 1, 198, 65, 191, 177, 37, 113, 35, 201,
                64, 131, 196, 92, 220, 13, 217, 119, 103, 77, 205, 176, 16, 136, 217, 241, 180, 224, 6, 80, 106, 94,
                154, 99, 227, 189, 91, 37, 229, 252, 50, 45, 174, 3, 16, 42, 57, 64, 238, 188, 107, 170, 182, 31, 166,
                171, 176, 204, 170, 137, 119, 48, 209, 249, 63, 209, 78, 234, 88, 187, 157, 156, 72, 212, 85, 247, 246,
                237, 171, 255, 129, 248, 61, 116, 98, 91, 29, 119, 130, 197, 80, 68, 190, 130, 150, 173, 163, 113, 113,
                71, 115, 208, 175, 53, 160, 144, 16, 149, 253, 230, 56, 16, 20, 12, 155, 106, 93, 181, 232, 76, 37, 158,
                117, 85, 130, 36, 255, 76, 219, 31, 22, 53, 184, 213, 114, 89, 176, 117, 39, 203, 217, 62, 116, 7, 242,
                63, 58, 128, 147, 158, 28, 130, 187, 227, 128, 1, 201, 107, 32, 214, 141, 170, 106, 247, 65, 102, 255,
                231, 191, 13, 84, 58, 4, 113, 164, 87, 67, 206, 64, 201, 36, 6, 187, 239, 132, 2, 16, 187, 109, 182,
                143, 237, 55, 126, 185, 189, 111, 108, 153, 174, 21, 255, 169, 45, 53, 151, 40, 214, 225, 86, 144, 202,
                209, 149, 211, 9, 94, 193, 94, 129, 23
            };
            byte[] p =
            {
                231, 163, 29, 139, 11, 6, 106, 184, 190, 126, 22, 155, 215, 249, 90, 10, 34, 95, 154, 93, 7, 64, 251,
                195, 22, 94, 102, 10, 243, 177, 67, 50, 117, 183, 91, 184, 246, 63, 156, 28, 134, 114, 81, 90, 219, 27,
                161, 146, 91, 181, 252, 152, 145, 10, 46, 124, 124, 100, 225, 22, 168, 217, 50, 45, 206, 245, 127, 187,
                146, 91, 43, 69, 218, 5, 185, 171, 38, 60, 224, 149, 16, 224, 180, 184, 81, 124, 250, 158, 120, 201,
                110, 124, 18, 239, 190, 159, 244, 106, 133, 65, 10, 39, 220, 202, 146, 1, 63, 74, 176, 51, 23, 151, 41,
                153, 147, 213, 179, 229, 50, 90, 90, 126, 162, 236, 84, 176, 13, 163
            };
            byte[] q =
            {
                231, 85, 13, 218, 199, 119, 199, 252, 63, 168, 10, 200, 68, 90, 15, 176, 17, 139, 130, 185, 117, 227,
                74, 222, 122, 201, 228, 20, 64, 195, 41, 94, 105, 189, 29, 229, 144, 169, 170, 254, 85, 146, 88, 5, 57,
                19, 216, 149, 3, 103, 46, 249, 38, 97, 74, 50, 150, 167, 52, 239, 90, 88, 230, 9, 111, 243, 134, 168,
                34, 50, 232, 160, 56, 174, 54, 93, 9, 47, 202, 67, 183, 102, 21, 165, 138, 168, 204, 187, 227, 152, 194,
                244, 183, 189, 183, 1, 23, 46, 249, 30, 248, 66, 235, 229, 61, 65, 112, 59, 176, 136, 52, 173, 204, 21,
                195, 77, 17, 195, 202, 40, 111, 67, 248, 39, 142, 13, 77, 253
            };

            EncryptionKeyParameters keyParameters = new EncryptionKeyParameters
            {
                E = exponent,
                M = mod,
                P = p,
                Q = q,
                D = d,
                DP = dp,
                DQ = dq,
                IQ = inverseQ,
            };

            RSAParameters rsaParameters = (RSAParameters) keyParameters;

            Assert.Equal(exponent, rsaParameters.Exponent);
            Assert.Equal(mod, rsaParameters.Modulus);
            Assert.Equal(p, rsaParameters.P);
            Assert.Equal(q, rsaParameters.Q);
            Assert.Equal(d, rsaParameters.D);
            Assert.Equal(dp, rsaParameters.DP);
            Assert.Equal(dq, rsaParameters.DQ);
            Assert.Equal(inverseQ, rsaParameters.InverseQ);
        }

        const string PrivateKeyPem = @"-----BEGIN RSA PRIVATE KEY-----
            MIIEpAIBAAKCAQEA0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyC
            AcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDu
            vGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2j
            cXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfy
            PzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03
            frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFwIDAQABAoIBAQCetcR46XYrLeIe
            7Trv2yolGDRlmfAzfZOnogXE0YkRfouLKyb4aXcY/hzBuLy0KjUNo9zsc1jk6X0C
            TIHUf60NnJPKv43V9JHUHponAfPbZrpdRxq9y3VZOo6JyDinZiv13H/9H5uSnU1E
            qGegeL7XgEbWKZosHr8lDya7BRljxSD8owxOJUoplkFSErQ/T/zUiPwFvm47rkC/
            RcnPPnlh17T3jg5ko1ks8zqo953Rc+lAV0DqONRnOfpuM0msSdoHpRo1drLRwUId
            h+okP5uTI4xHdWMpjyV2AS3aLDkdh55p1ImXO9bgI1D/G68J93D9Pr7Zo83f5Gde
            XgJu5AkhAoGBAOejHYsLBmq4vn4Wm9f5WgoiX5pdB0D7wxZeZgrzsUMydbdbuPY/
            nByGclFa2xuhklu1/JiRCi58fGThFqjZMi3O9X+7klsrRdoFuasmPOCVEOC0uFF8
            +p54yW58Eu++n/RqhUEKJ9zKkgE/SrAzF5cpmZPVs+UyWlp+ouxUsA2jAoGBAOdV
            DdrHd8f8P6gKyERaD7ARi4K5deNK3nrJ5BRAwyleab0d5ZCpqv5VklgFORPYlQNn
            LvkmYUoylqc071pY5glv84aoIjLooDiuNl0JL8pDt2YVpYqozLvjmML0t723ARcu
            +R74QuvlPUFwO7CINK3MFcNNEcPKKG9D+CeODU39AoGAKZYZWbsy/boJSS3Z8N5t
            keA19cq54KrSjZOJEnJJ4tyOUcr/3AXzixOANqbvK3jIg/qaTPHNOCdVVe8rWEkW
            Py8m2DXewst0EP5yJQ4KY++fRhhr9wVPIWBiGZng9HXu1bzCC7k9CuC7ccnhKN0j
            YRow3l/BmmZ93j1aFr/lk60CgYBjbkqDM+NHQSP3TZMg1fkSO6hUavTB2mdgLbDs
            B54bBOq8D8KheFv37730OWJ8JkZ+bPZivt1ob/ATNIyAr99IRSdORKxWZ2ielDrn
            qFAzRwHoTfuWatF9HOmHOnpTf/pnBZiseBcDn8fBfcUaLqE95o+gH1s4ZYcVtAhQ
            sB0F3QKBgQDTXnepfBSQw3cVBGVYjwbBxQxoL9GBUTMhjRaQfqUBWGJVMSaxib+6
            dAbaMIAVw0DIMKG8tTYNEjEpyGuHvSht4vZRgarCNqDk13EKY80LkyOv2H/J01rt
            gaZk6+H62W5zGnIbtzodB2n7JasK561Ic/QcrEtheC4Qmr+RXe03pg==
            -----END RSA PRIVATE KEY-----
        ";
    }

    public struct EncryptionKeyParameters
    {
        public byte[] E;
        public byte[] M;
        public byte[] P;
        public byte[] Q;
        public byte[] D;
        public byte[] DP;
        public byte[] DQ;
        public byte[] IQ;

        public static explicit operator EncryptionKeyParameters(RSAParameters parameters) =>
            new EncryptionKeyParameters
            {
                E = parameters.Exponent,
                M = parameters.Modulus,
                P = parameters.P,
                Q = parameters.Q,
                DP = parameters.DP,
                DQ = parameters.DQ,
                IQ = parameters.InverseQ,
                D = parameters.D,
            };

        public static explicit operator RSAParameters(EncryptionKeyParameters parameters) =>
            new RSAParameters
            {
                Exponent = parameters.E,
                Modulus = parameters.M,
                P = parameters.P,
                Q = parameters.Q,
                DP = parameters.DP,
                DQ = parameters.DQ,
                InverseQ = parameters.IQ,
                D = parameters.D,
            };
    }
}
