using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Telegram.Bot.Tests.Unit.Passport
{
    internal static class EncryptionKey
    {
        public static RSA RsaPrivateKey
        {
            get
            {
                string privateKeyPem = @"-----BEGIN RSA PRIVATE KEY-----
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
                PemReader pemReader = new PemReader(new StringReader(privateKeyPem));
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair) pemReader.ReadObject();
                RSAParameters parameters =
                    DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
                RSA rsa = RSA.Create(parameters);
                return rsa;
            }
        }
    }
}
