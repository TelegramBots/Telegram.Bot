//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using Newtonsoft.Json;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.OpenSsl;
//using Org.BouncyCastle.Security;
//using Telegram.Bot.Passport;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Passport;
//using Xunit;
//
//namespace UnitTests.Decryption
//{
//    public class PassportTests2
//    {
//        [Fact]
//        public void Should_decrypt_credentials()
//        {
//            RSA key = GetRsaKey();
//            Update update = GetPersonalDetailsUpdate();
//            PassportData passData = update.Message.PassportData;
//
//            IDecrypter decrypter = new Decrypter(key);
//
//            Credentials credentials = decrypter.DecryptCredentials(passData.Credentials);
//
//            Assert.NotNull(credentials);
//
////            Assert.Equal("my payload", credentials.Nonce); // ToDo
//            Assert.Equal("my payload", credentials.Payload);
//
//            Assert.NotNull(credentials.SecureData);
//            Assert.NotNull(credentials.SecureData.PersonalDetails);
//            Assert.NotNull(credentials.SecureData.PersonalDetails.Data);
//            Assert.NotEmpty(credentials.SecureData.PersonalDetails.Data.Secret);
//            Assert.NotEmpty(credentials.SecureData.PersonalDetails.Data.DataHash);
//        }
//
//        [Fact]
//        public void Should_decreypt_personal_details()
//        {
//            RSA key = GetRsaKey();
//            Update update = GetPersonalDetailsUpdate();
//
//            IDecrypter decrypter = new Decrypter(key);
//
//            // ToDo use a class fixture
//            Credentials credentials = decrypter.DecryptCredentials(update.Message.PassportData.Credentials);
//
//            PersonalDetails personalDetails = decrypter.DecryptElementData<PersonalDetails>(
//                update.Message.PassportData.Data.Single(),
//                credentials.SecureData
//            );
//
//            Assert.Equal("AF", personalDetails.CountryCode);
//        }
//
//        static RSA GetRsaKey()
//        {
//            string privateKeyPem = @"-----BEGIN RSA PRIVATE KEY-----
//MIIEpAIBAAKCAQEA0VElWoQA2SK1csG2/sY/wlssO1bjXRx+t+JlIgS6jLPCefyC
//AcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTgBlBqXppj471bJeX8Mi2uAxAqOUDu
//vGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX39u2r/4H4PXRiWx13gsVQRL6Clq2j
//cXFHc9CvNaCQEJX95jgQFAybal216EwlnnVVgiT/TNsfFjW41XJZsHUny9k+dAfy
//PzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6BHGkV0POQMkkBrvvhAIQu222j+03
//frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6BFwIDAQABAoIBAQCetcR46XYrLeIe
//7Trv2yolGDRlmfAzfZOnogXE0YkRfouLKyb4aXcY/hzBuLy0KjUNo9zsc1jk6X0C
//TIHUf60NnJPKv43V9JHUHponAfPbZrpdRxq9y3VZOo6JyDinZiv13H/9H5uSnU1E
//qGegeL7XgEbWKZosHr8lDya7BRljxSD8owxOJUoplkFSErQ/T/zUiPwFvm47rkC/
//RcnPPnlh17T3jg5ko1ks8zqo953Rc+lAV0DqONRnOfpuM0msSdoHpRo1drLRwUId
//h+okP5uTI4xHdWMpjyV2AS3aLDkdh55p1ImXO9bgI1D/G68J93D9Pr7Zo83f5Gde
//XgJu5AkhAoGBAOejHYsLBmq4vn4Wm9f5WgoiX5pdB0D7wxZeZgrzsUMydbdbuPY/
//nByGclFa2xuhklu1/JiRCi58fGThFqjZMi3O9X+7klsrRdoFuasmPOCVEOC0uFF8
//+p54yW58Eu++n/RqhUEKJ9zKkgE/SrAzF5cpmZPVs+UyWlp+ouxUsA2jAoGBAOdV
//DdrHd8f8P6gKyERaD7ARi4K5deNK3nrJ5BRAwyleab0d5ZCpqv5VklgFORPYlQNn
//LvkmYUoylqc071pY5glv84aoIjLooDiuNl0JL8pDt2YVpYqozLvjmML0t723ARcu
//+R74QuvlPUFwO7CINK3MFcNNEcPKKG9D+CeODU39AoGAKZYZWbsy/boJSS3Z8N5t
//keA19cq54KrSjZOJEnJJ4tyOUcr/3AXzixOANqbvK3jIg/qaTPHNOCdVVe8rWEkW
//Py8m2DXewst0EP5yJQ4KY++fRhhr9wVPIWBiGZng9HXu1bzCC7k9CuC7ccnhKN0j
//YRow3l/BmmZ93j1aFr/lk60CgYBjbkqDM+NHQSP3TZMg1fkSO6hUavTB2mdgLbDs
//B54bBOq8D8KheFv37730OWJ8JkZ+bPZivt1ob/ATNIyAr99IRSdORKxWZ2ielDrn
//qFAzRwHoTfuWatF9HOmHOnpTf/pnBZiseBcDn8fBfcUaLqE95o+gH1s4ZYcVtAhQ
//sB0F3QKBgQDTXnepfBSQw3cVBGVYjwbBxQxoL9GBUTMhjRaQfqUBWGJVMSaxib+6
//dAbaMIAVw0DIMKG8tTYNEjEpyGuHvSht4vZRgarCNqDk13EKY80LkyOv2H/J01rt
//gaZk6+H62W5zGnIbtzodB2n7JasK561Ic/QcrEtheC4Qmr+RXe03pg==
//-----END RSA PRIVATE KEY-----
//";
//            PemReader pemReader = new PemReader(new StringReader(privateKeyPem));
//            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair) pemReader.ReadObject();
//            RSAParameters parameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);
//            RSA rsa = RSA.Create(parameters);
//            return rsa;
//        }
//
//        static Update GetPersonalDetailsUpdate() =>
//            JsonConvert.DeserializeObject<Update>(@"
//{
//    ""update_id"": 1,
//    ""message"": {
//        ""message_id"": 2,
//        ""from"": {
//            ""id"": 3,
//            ""is_bot"": false,
//            ""first_name"": ""Tester"",
//            ""language_code"": ""en-us""
//        },
//        ""chat"": {
//            ""id"": 3,
//            ""first_name"": ""Tester"",
//            ""type"": ""private""
//        },
//        ""date"": 1535413111,
//        ""passport_data"": {
//            ""data"": [{
//                ""type"": ""personal_details"",
//                ""data"": ""ZKiEn/pGfpOGjBptx9jKVID0xyxRzdT9cExgKXoG22S+xd0/VR7fSOwiA3iGfx37EOIYlmHH+3ANW4szfMA4ctrEXZOpc08QAhMaZz2ZttNqtYywLM/pVRS+0ENvK/N1YRZUDLawzHktobslR6voQrTF5q1db1NvDmafVyuPVGYyfHNM9ZHJqPOLXfGTAcdsUZ64bKJK1u4hNQQMjqcA+RBObiwxrWLod7Xu1aHNfdsDKDVI+F3mXh4O2M7xTchAl/gi10Fq/JN6C6Z77hDJmFIUlOXi4H0vQMufhC+m19NVb84dnnkZuYf0XKfw0HSUGTml4UjmkZJatPC6w7G+GVA+XvDhV3Jbea3wFoDE5tmqJ/qo56k3qjDvFteTksMPY21EWrpLXSB9kqkmo9JbBQyLFsGV5isNZeXfw4XqdtQB7oNA3NYvEkmEKoygW9YJ"",
//                ""hash"": ""Gf8jR8d28qB964tYdyWvoleB+q8rcviyGTa7VD6TxlA=""
//            }],
//            ""credentials"": {
//                ""data"": ""7AJqzQ3DxFwbPQwVLU1yNOfkiu7ulDEqKwqHp2cWYckDODmFW0a1tO0iOJvbU/a1HBKgFvJZ9bhKJHpxJf+EmdPC8dZKfbXR80r/+dK6tk8HhOmEQs/fikSO4tdmyzkdJPNH7+j3I3knW9NVFeN+YXgp+ZR93iJ5EMsZfDM/BZ23AW+I6HgO2xyZA/w5xkCe355M10VlG6ofQomKpyP6aVXchsRRlVnzvSWI+CtyR67SFBFx6ugecSTCizJmEHT3huKm4wZub3GluZA9TjjHFFKmL6iNq8noxQwqB139FQj3Uwlg+unlUDRMJRdnW46TU2VDfwHMuLUZx+lbs56UXj0rPS3/U8sjdjliSz3q4Ob5mDAVWAxPvM9xpgnxsCqhkuvWeIxVSE+BF6FF/vv/RlJ694Rkh80Cs0560WPgbDdBXpPZfPzBYbd6/LLwmjvFihoHgrPg1sNrKoGgIUSWiOOrV/yKOBq5JzwwtelQ+fJG8AQz7wvOTOxXvq2qoxYv"",
//                ""hash"": ""tglPsbBhfUZ4AKodwEpd39oBC3tbgi93oQW/527Ty9A="",
//                ""secret"": ""m6QIjXsRyFxxFRdD6aXko4LuquD5p0yCX2ohSLDLvdGYSRRg/Ebpv3nqUSa75+B7uSbEe41zkvGcUY6VQgfDC6HydW6OM/wmXr3iKUKZ697XO8jXBy6IoHXXnTN/CdiWNsxBmZhEelzY+W3eWP+0hBlVfoKpR9+p+ciGqsHRCiYsaDh6K/8hSmBHI54PeqfqmZOt+lb+ho12xCyvtqX8JHlwhyvlmyz5hhyJCazYiEWtNN4vjBpK1ff3HO3xTs+PBDQfrblGvvQ1zvjVw0ka+1uzXabavI/7ZGZBCBiYS13mYZWo8gbqDjEsbZ4FlRXy9iU1zrBTe96ThquMoWpnOw==""
//            }
//        }
//    }
//}
//        ");
//    }
//}
