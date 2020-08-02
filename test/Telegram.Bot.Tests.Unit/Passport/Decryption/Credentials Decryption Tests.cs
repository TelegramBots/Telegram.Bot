// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using System;
using System.Security.Cryptography;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Passport;
using Telegram.Bot.Tests.Unit.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for decrypting credentials using <see cref="IDecrypter.DecryptCredentials"/> method
    /// </summary>
    public class CredentialsDecryptionTests
    {
        [Fact(DisplayName = "Should decrypt credentials for personal_details scope")]
        public void Should_Decrypt_Credentials_Personal_Details()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "tHcn5IEhx7REdkb/C9BTEJW7Ftob4UFzl/vWQXADBqfTCG05OvvgMn6GYZQpi8qW92tREsju35adGvzX6+lJrcSZYPr3" +
                       "sRbok+2lBIBs/tIeGWl39HpTTHhQsMTCILnOsuqpJzYAq0TvbTcaq2rkD8qTG30fxbVNWpQbRJCvFkLH3ueuJfMHs/ig" +
                       "P85QsO1sjz4915ZOPbsh9VR3x3dS+pKM+LCB4sQs2/o8Qy6jES1ZIHckTRNHNBfKeMnzlOPbTZHjJvAJ4B0P8sCpbzKQ" +
                       "M/buRZhpLRsv5Pe9U61UNALSg/Vq98st41WKH35CaLME+dwHvO4a+xCO78GnySjNjCPsCjCqEWHEXtUtbodZsMw4sdje" +
                       "rwfC3LBpPJygjn8pAwyt2LjqRSjtwxqW86AdkkFpAW4qJJ2Uy70onxtY2M97yYXRkizIt6y/sLvkh2mRWW917lUhdTf/" +
                       "M3YaTK6kiQXhWPTX/78U8AtXvhiw07iMRxVwRmHKyAVyI334C3ZKiY0rscRAVwYlrCHFtVcxMQ==",
                Hash = "QN3IRzvFR9k/yvDfi9ChnQtIHo8No2SZm3iGwwj4NVk=",
                Secret = "Sr/17/6JrtKCP7X/e9c7XIMAdigeI1QO6u43prhnS9wuNralsZhvPnKIc3qL7A2jcgML273TM2blHywbzt6cAqLxjC" +
                         "ntyjSay0FyMnctarY3soCkCZsUynMsPC9g39CTVBCUXbZZ6tWZ8mgQ9WDXeMVTRaLXLBr9EZdICauFGsln/LaopfU9" +
                         "CvdYXQ0PcdhCFNbisuPwXOqd5jUu0x49+sPAc4V68TsnWRUC3CYEhEfqkRmtomM8UV+/JyHk0zYdiRxarGzAXfgdXJ" +
                         "wjfjXARhERA/hZRYKH+w9vsPpZWdqQg7zSi5EU8Fr2Cs3IzAes+txLUekFprWsKff7j21KXg=="
            };

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(encryptedCredentials, EncryptionKey.RsaPrivateKey);

            Assert.NotNull(credentials);
            Assert.Equal("TEST", credentials.Nonce);
            Assert.NotNull(credentials.SecureData);
            Assert.NotNull(credentials.SecureData.PersonalDetails);

            Assert.NotNull(credentials.SecureData.PersonalDetails.Data);
            Assert.Equal(
                "v9Hx0oaQHLxyGuZdiYmszC3gTGyfYnc57zWLg+WaMus=",
                credentials.SecureData.PersonalDetails.Data.Secret
            );
            Assert.Equal(
                "IeyXbEWBTXXQYG+O0vv7munuGs0H0S4Jr7jzYV1ltCk=",
                credentials.SecureData.PersonalDetails.Data.DataHash
            );

            Assert.Null(credentials.SecureData.DriverLicense);
            Assert.Null(credentials.SecureData.Address);
            Assert.Null(credentials.SecureData.Passport);
            Assert.Null(credentials.SecureData.BankStatement);
            Assert.Null(credentials.SecureData.IdentityCard);
            Assert.Null(credentials.SecureData.InternalPassport);
            Assert.Null(credentials.SecureData.PassportRegistration);
            Assert.Null(credentials.SecureData.RentalAgreement);
            Assert.Null(credentials.SecureData.TemporaryRegistration);
            Assert.Null(credentials.SecureData.UtilityBill);
        }

        [Fact(DisplayName = "Should decrypt credentials for driver_license scope with selfie and one translation")]
        public void Should_Decrypt_Credentials_Driver_License()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "nh/lJN9e0kATdoWVU01ellh1Hdl4IkqkJIxtIUMwi37wx5cTsZC0d2NFOs6W2WYaGkOzvxyvAEuIK5UlVS05Wyjr7cMI" +
                       "mY7hcO5v8FZyIXCs1PGnOEBr+U3kB7adWoAIKyfcBlQlTnqlRJaUXoqTZuErqpcI/XBAZWMSj09oQ4vxB7ReQxQxLLTw" +
                       "3PHsfl6Oij52tvQrjtfMQSChyEB2BS29xmykn/cx3Bx8866x2HSlD47MU8NxQR0rxwesXzqS6sz7IJh/iRbJVpCeSRxg" +
                       "Gtczrks7qT33GUlGd7uLOqv/vss5Yhn08Vo3nwemew3/JPoJ0ZORYa5+CR4L/SZqtGNIdhqXLWhvGyPa6VW3VrS27juz" +
                       "HarGQDiEBBO1+TzQ/uPyEmLcurcJ1d8uOnD61lIkyVzd6E0IGfRw/VPv0H0BJpKbh81hD012ESl4CxKLnnWBbwkqIiIm" +
                       "CLmMZMp8bxlQn4oV1MXhSqVEODUVyThKq6+kAYBMX6Nse/w0xt0UJxwfKojYcWnBR3SSSBAr3TZOjguZ8DPIixVGDgKS" +
                       "ArmgEdYx71XYrfCjM97iDJ/3BJENTxHX7zfbCXBrt41HhCsZ09MaWFlWUUivMDelCsxY7a/yiODkh1rFdTjzCm11s8kv" +
                       "p9wEXU8EXSqNJ0BoC+AbIgdp4n3r0hjbPkPvN+0dAuBXHPpCfMfJRSadscAnWJG0NTnfIbE9GLcOCIUt90d0Fx1yoCNE" +
                       "mMFUiifofOzpr5nvirAU6cHNESRh/cCJTnMF21YuE6wPR0TK3f9326sG4xmYE65gya9xVNZaA7TSME9w7M9gADDERcAU" +
                       "PqUlQVFejWdT/VfTDcFCgpcGNnF4MSP8dWMmOWBYQOJU9WgK1xzftrL30et8w/OAFFsYx3eOVPnnby0KVeEZFj4AI2BQ" +
                       "u46JBaKjE0+gTU+7KC49icXzleVniMSLF+5jUO964ZGEOuiubmEmc+Ncxvs5C2+c5zcptGq75JBd6jHxMNdLCxUa5eub" +
                       "ShysT+1VSxOepemjvHKIkxkdFMtr3r0rs0ilRBw4tbspoVcPZ1/abb/KD99qYqIse8H37OmWm2LcQnknZvi8TsFNAPaW" +
                       "4+2+Ko2E1sfMrvB3+EJMGyUhqkvvC5XRYvK7kXr3xrxX48w9",
                Hash = "y8fTLZW7Gvf1c25tQY/bMjzbnGpnfGHPKnEC9Vk39Us=",
                Secret = "J4j2cRBWuNLRc6yXsCL8RgzKSDZAlS27uFqFw3pMs+w3ScHDLcQgk/6+QidKSAzX0EccS6rbZ0UTDoSEptvdUT61A4" +
                         "hqMG61kbczf0UAopVQAeqlTkbZfgiUUXj5hpAKJI2Z/o78UWzRH6hoFhqPN1T+zs4FAhBEbv6nF1K2Rav8SOmE5OXa" +
                         "7B4a31FhH/1b47uAT1AxskzJZ6LjY6UrgkHU4/em413L0Boyl/nh1PNmgoTFCd3S+CnujpyZW67rBuNodFzJAEzXTe" +
                         "8M4bm/diGXNjht+mq0vB8dnwkGcNKFNVv6wWqvNWY8AZdJDdZChW+N4weATQGUAAgNQax1Tw=="
            };

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(encryptedCredentials, EncryptionKey.RsaPrivateKey);

            Assert.NotNull(credentials);
            Assert.Equal("TEST", credentials.Nonce);
            Assert.NotNull(credentials.SecureData);
            Assert.NotNull(credentials.SecureData.DriverLicense);

            Assert.NotNull(credentials.SecureData.DriverLicense.Data);
            Assert.Equal(
                "OUbagqviVbzbJZugFdTZDU4Yh6cPTLC9sWuyGOGKRGw=",
                credentials.SecureData.DriverLicense.Data.Secret
            );
            Assert.Equal(
                "v1Kxeb2E6ZAdnfsIRy7C2yas3ssTw2qP4274QCRgHPA=",
                credentials.SecureData.DriverLicense.Data.DataHash
            );

            Assert.NotNull(credentials.SecureData.DriverLicense.FrontSide);
            Assert.Equal(
                "a+jxJoKPEaz77VCjRvDVcYHfIO3+h+oI+ruZh+KkYa0=",
                credentials.SecureData.DriverLicense.FrontSide.Secret
            );
            Assert.Equal(
                "THTjgv2FU7kff/29Vty/IcqKPmOGkL7F35fAzmkfZdI=",
                credentials.SecureData.DriverLicense.FrontSide.FileHash
            );

            Assert.NotNull(credentials.SecureData.DriverLicense.ReverseSide);
            Assert.Equal(
                "pBkfn4122dJSJRyhYCP03d9/BBA3oRvWVtqPDx/qrXE=",
                credentials.SecureData.DriverLicense.ReverseSide.Secret
            );
            Assert.Equal(
                "LgS7DLrLslUqgKftFPQ2GJj/T54Fti17qKTmd61kOmw=",
                credentials.SecureData.DriverLicense.ReverseSide.FileHash
            );

            Assert.NotNull(credentials.SecureData.DriverLicense.Selfie);
            Assert.Equal(
                "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
                credentials.SecureData.DriverLicense.Selfie.Secret
            );
            Assert.Equal(
                "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                credentials.SecureData.DriverLicense.Selfie.FileHash
            );

            Assert.NotNull(credentials.SecureData.DriverLicense.Translation);
            FileCredentials translationCredentials = Assert.Single(credentials.SecureData.DriverLicense.Translation);
            Assert.NotNull(translationCredentials);
            Assert.Equal("y/la59gA5ZQe7A+2pOQH1Jp+LwurLw3HOvpjrvnWvoQ=", translationCredentials.Secret);
            Assert.Equal("sImKv6vUZhj7J10pwpJW7pSykUQJ2NpIetmGrTqbyy8=", translationCredentials.FileHash);

            Assert.Null(credentials.SecureData.DriverLicense.Files);

            Assert.Null(credentials.SecureData.PersonalDetails);
            Assert.Null(credentials.SecureData.Address);
            Assert.Null(credentials.SecureData.Passport);
            Assert.Null(credentials.SecureData.BankStatement);
            Assert.Null(credentials.SecureData.IdentityCard);
            Assert.Null(credentials.SecureData.InternalPassport);
            Assert.Null(credentials.SecureData.PassportRegistration);
            Assert.Null(credentials.SecureData.RentalAgreement);
            Assert.Null(credentials.SecureData.TemporaryRegistration);
            Assert.Null(credentials.SecureData.UtilityBill);
        }

        [Fact(DisplayName = "Should decrypt credentials for identity_card and utility_bill scopes")]
        public void Should_Decrypt_Credentials_ID_Card_Utility_Bill()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "8QKNwpmjaLNWOwK1Hqp0luB9/q/qSujDA5pGid1d7tyokaDn+6Y1RWFOGXBYoQMonm1kmIFmsw4iu8LjKMm4SZpH5bqS" +
                       "PMiuD7j7O2FPE5A99A/TrDsNCNzeu+TVKNBWInSrks3wsmITMLT3WPvVaXFeolCXugJChwgY+cnmpUecitbqQxR/9Xpz" +
                       "TQapyIuKWdhWhcL8fS3fdCyojw5b7BWAxOXba7zuWA+nsKX1V7v1puMDiNHwDuIJGPl3mbAJ24afqdlT4UJXaVw9XWVb" +
                       "17c7g68qzaEqMaIb3OR1ydrE7vKpHHvFWSaEENoz2wv0PnDA0jyWMOnoL42HbVbu06XJoHuDSsFR2Rx2Hm7KZ1QM/OhD" +
                       "bpEiEa2+jJ1At9jftNL3JwEpktLAso5UQJbqCPgAPhowD9l/yvzUa0MRU+2P+lzZPDUNsZiy0wi0QhJafaHOg0mmjlao" +
                       "Nw37ebpPDCfF71NzoV4cetIv5YE2wauaN+20NQyPyUDlHKhpeEpsBVfpw0jGpannopX+aBwKEIANqlLto0HCY8/o81kf" +
                       "kgBqxqT7vKfr7Xm1glYwWNa9gNws4NEQZ2mCvZb3CyCsZL1b0GWc3O66F140rus+fO6Kz5jlc7ASVamsBytwvhTwnW/T" +
                       "llYTFRArNRRStlKDMxADKIdeTzVBWIRPxqEuI7TTT5r1gZBi7NW9to7nbMCoXjoLEdNOjtiRLgb5TyDjRXgdHbfKg428" +
                       "Q+Lvu1BlxGwTJuPQAwBdgpQKBoAmypix8BIHZBz0SBXozOq+KgHXWKX5tu5zDu86x1BMgl/l4KuqAz8NtPEB6VISPExu" +
                       "UxhDzWYqpI0yjFrKnE1aRX6I/gif4cQhkl+2eqUquYuxszTPrQJ4BhNsB7KR4x8WNcNm59xSjA1TxrNkJ6xcen7DNi0+" +
                       "bApRcPWMB+0dYBPXD1cFKo9he0fp9EpPN1tdvZQNpa4/LzjpwlbuX5bUSWWnlBKT60gMqtbzbRWC96deOHB11jTmvfFP" +
                       "+uTutO17Z3Gr9jIeC4cLjU5jcJh40KoNw3NHkIXx3ckixAvrGIsfkjDZrQa8ALqY8XXxgq7CaM8zYrbn9DU5rx5+MNa/" +
                       "IjpSDEELD4pFiPYUfm1Z4lF/N3pWDkiKZ10z+iPDigdIbdHcI7TPCDmpcHEHxYHvNG6GA2KLnliA0g3zLs6q+K8WnaqL" +
                       "Zq4XqknT/98NvfjBm4OSjnnVmXEEz60G+1I7oRrZ0A==",
                Hash = "TMUO1tE81hIOCgaqN7buhqG8SIZHjFfJrD93LNKL4Yg=",
                Secret = "QcOxuwd9OiB/9akjMzyY7wR4NcrpbhpjQuO9yOWhe0u34VVLraTr3gwkBNv0eKEZHoyulhhLr9tkSSO+BYZAp4engu" +
                         "ed3eL11jqQkosJQBCPg8m1arIvNM+/E5Kw8dnF7dEx9v8t9QA11kSfAqdgnqCtSAq6GGGu5ixuYM1VMbk270qcm3F7" +
                         "wrLN+9YQwUVkiai8WvdA7Q7BnywsbrekKOam95tiFeA7jE8Cf78D6gh47/uirO/KD3Hwl1PNo1f8ORgFf8EixSQuV5" +
                         "Gh8HxEY1uE+yfOxksG5MiWOC5A1lNQuVcZqzVfbReRvs2M2tvX5KeeN+/xsIps+Xp+szWSaw=="
            };

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(encryptedCredentials, EncryptionKey.RsaPrivateKey);

            Assert.NotNull(credentials);
            Assert.Equal("TEST", credentials.Nonce);
            Assert.NotNull(credentials.SecureData);
            Assert.NotNull(credentials.SecureData.IdentityCard);
            Assert.NotNull(credentials.SecureData.UtilityBill);

            Assert.NotNull(credentials.SecureData.IdentityCard.Data);
            Assert.Equal(
                "N4kE1ol2nNr072G9K3oX/+3rq6ma9zu6qIjc7XmWvZo=",
                credentials.SecureData.IdentityCard.Data.Secret
            );
            Assert.Equal(
                "ygNgkwXnr0MrkVO4Ru5q6GmmFQ9TKDrJYwcQkkGBxLk=",
                credentials.SecureData.IdentityCard.Data.DataHash
            );

            Assert.NotNull(credentials.SecureData.IdentityCard.FrontSide);
            Assert.Equal(
                "gK4F8D7+rvWIo+qNDdfxmq3KDTB6XQZIu4aXYPaNOf4=",
                credentials.SecureData.IdentityCard.FrontSide.Secret
            );
            Assert.Equal(
                "lLAReRFMP3vWc6j2Cmr00/lKeEnDnHRK2enpNwbQ+Wk=",
                credentials.SecureData.IdentityCard.FrontSide.FileHash
            );

            Assert.NotNull(credentials.SecureData.IdentityCard.ReverseSide);
            Assert.Equal(
                "7BRjRIe3N86vXWkc12dKRrDtpoG1d5IfgvLIvGyO/gU=",
                credentials.SecureData.IdentityCard.ReverseSide.Secret
            );
            Assert.Equal(
                "/TLkaj2kgjL5/NmZiZMp1h0B1ZAUQeLLAcpZam6nW4w=",
                credentials.SecureData.IdentityCard.ReverseSide.FileHash
            );

            Assert.NotNull(credentials.SecureData.IdentityCard.Selfie);
            Assert.Equal(
                "5pLOfRfSFvHofKI0v51EPSAUioipLLbHVBMZ+tPI/2k=",
                credentials.SecureData.IdentityCard.Selfie.Secret
            );
            Assert.Equal(
                "vuyqgh8ZXt2p43vJ1u14qzcTfRUEuj4oSG4BO0JJle0=",
                credentials.SecureData.IdentityCard.Selfie.FileHash
            );

            Assert.Null(credentials.SecureData.IdentityCard.Files);
            Assert.Null(credentials.SecureData.IdentityCard.Translation);

            Assert.NotNull(credentials.SecureData.UtilityBill.Files);
            FileCredentials billFileCredentials = Assert.Single(credentials.SecureData.UtilityBill.Files);
            Assert.NotNull(billFileCredentials);
            Assert.Equal("SVH5PR7vKkjRCL9NpNRqV5DX9jqDWbW8f5SdjqcrUS4=", billFileCredentials.Secret);
            Assert.Equal("Yek2IalAvcaOanrWzBRB2AU7kBdgCleELUWeL7dpkuM=", billFileCredentials.FileHash);

            Assert.NotNull(credentials.SecureData.UtilityBill.Translation);
            FileCredentials translationCredentials = Assert.Single(credentials.SecureData.UtilityBill.Translation);
            Assert.NotNull(translationCredentials);
            Assert.Equal("R7vXeQE6xv4vmBInReyIJM+HqWu+QzPjxSmlkuHScOg=", translationCredentials.Secret);
            Assert.Equal("beHjnDS8SRXQYDLUOKERJXhBTFIdFQWYvjNdteYRF+I=", translationCredentials.FileHash);


            Assert.Null(credentials.SecureData.UtilityBill.Data);
            Assert.Null(credentials.SecureData.UtilityBill.FrontSide);
            Assert.Null(credentials.SecureData.UtilityBill.ReverseSide);
            Assert.Null(credentials.SecureData.UtilityBill.FrontSide);

            Assert.Null(credentials.SecureData.PersonalDetails);
            Assert.Null(credentials.SecureData.DriverLicense);
            Assert.Null(credentials.SecureData.Address);
            Assert.Null(credentials.SecureData.Passport);
            Assert.Null(credentials.SecureData.BankStatement);
            Assert.Null(credentials.SecureData.InternalPassport);
            Assert.Null(credentials.SecureData.PassportRegistration);
            Assert.Null(credentials.SecureData.RentalAgreement);
            Assert.Null(credentials.SecureData.TemporaryRegistration);
        }

        [Fact(DisplayName = "Should throw when null credentials is passed")]
        public void Should_Throw_If_Null_Credentials()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(null!, null!)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: encryptedCredentials$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null RSA key is passed")]
        public void Should_Throw_If_Null_Key()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(new EncryptedCredentials(), null!)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: key$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null credentials data is passed")]
        public void Should_Throw_If_Null_Credentials_Data()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(new EncryptedCredentials(), RSA.Create())
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: Data$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null credentials secret is passed")]
        public void Should_Throw_If_Null_Credentials_Secret()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "",
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: Secret$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null credentials hash is passed")]
        public void Should_Throw_If_Null_Credentials_Hash()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "",
                Secret = "",
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: Hash$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when credentials data string is empty")]
        public void Should_Throw_If_Empty_Credentials_Data_String()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "",
                Secret = "",
                Hash = "",
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );

            Assert.Matches(@"^Data is empty\.\s+Parameter name: Data$", exception.Message);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact(DisplayName = "Should throw when credentials data string has invalid length")]
        public void Should_Throw_If_Invalid_Credentials_Data_String_Length()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==",
                Secret = "",
                Hash = "",
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );

            Assert.Equal("Data length is not divisible by 16: 31.", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Theory(DisplayName = "Should throw when credentials secret is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public void Should_Throw_If_Invalid_Credentials_Secret(string secret)
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "dGV4dCBvZiAxNiBjaGFycw==",
                Secret = secret,
                Hash = "",
            };

            IDecrypter decrypter = new Decrypter();

            Assert.ThrowsAny<FormatException>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );
        }

        [Theory(DisplayName = "Should throw when credentials hash is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public void Should_Throw_If_Invalid_Credentials_Hash(string hash)
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "dGV4dCBvZiAxNiBjaGFycw==",
                Secret = "",
                Hash = hash,
            };

            IDecrypter decrypter = new Decrypter();

            Assert.ThrowsAny<FormatException>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );
        }

        [Theory(DisplayName = "Should throw when credentials hash length is not 32")]
        [InlineData("")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==")]
        [InlineData("Zm9v")]
        public void Should_Throw_If_Invalid_Credentials_Hash_Length(string hash)
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "dGV4dCBvZiAxNiBjaGFycw==",
                Secret = "",
                Hash = hash,
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );

            Assert.Matches(@"^Hash length is not 32: \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when trying to decrypt credentials with wrong RSA key")]
        public void Should_Throw_If_Decrypt_Credentials_Wrong_Key()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "tHcn5IEhx7REdkb/C9BTEJW7Ftob4UFzl/vWQXADBqfTCG05OvvgMn6GYZQpi8qW92tREsju35adGvzX6+lJrcSZYPr3" +
                       "sRbok+2lBIBs/tIeGWl39HpTTHhQsMTCILnOsuqpJzYAq0TvbTcaq2rkD8qTG30fxbVNWpQbRJCvFkLH3ueuJfMHs/ig" +
                       "P85QsO1sjz4915ZOPbsh9VR3x3dS+pKM+LCB4sQs2/o8Qy6jES1ZIHckTRNHNBfKeMnzlOPbTZHjJvAJ4B0P8sCpbzKQ" +
                       "M/buRZhpLRsv5Pe9U61UNALSg/Vq98st41WKH35CaLME+dwHvO4a+xCO78GnySjNjCPsCjCqEWHEXtUtbodZsMw4sdje" +
                       "rwfC3LBpPJygjn8pAwyt2LjqRSjtwxqW86AdkkFpAW4qJJ2Uy70onxtY2M97yYXRkizIt6y/sLvkh2mRWW917lUhdTf/" +
                       "M3YaTK6kiQXhWPTX/78U8AtXvhiw07iMRxVwRmHKyAVyI334C3ZKiY0rscRAVwYlrCHFtVcxMQ==",
                Hash = "QN3IRzvFR9k/yvDfi9ChnQtIHo8No2SZm3iGwwj4NVk=",
                Secret = "Sr/17/6JrtKCP7X/e9c7XIMAdigeI1QO6u43prhnS9wuNralsZhvPnKIc3qL7A2jcgML273TM2blHywbzt6cAqLxjC" +
                         "ntyjSay0FyMnctarY3soCkCZsUynMsPC9g39CTVBCUXbZZ6tWZ8mgQ9WDXeMVTRaLXLBr9EZdICauFGsln/LaopfU9" +
                         "CvdYXQ0PcdhCFNbisuPwXOqd5jUu0x49+sPAc4V68TsnWRUC3CYEhEfqkRmtomM8UV+/JyHk0zYdiRxarGzAXfgdXJ" +
                         "wjfjXARhERA/hZRYKH+w9vsPpZWdqQg7zSi5EU8Fr2Cs3IzAes+txLUekFprWsKff7j21KXg=="
            };

            IDecrypter decrypter = new Decrypter();

            Assert.ThrowsAny<CryptographicException>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, RSA.Create())
            );
        }

        [Fact(DisplayName = "Should throw when trying to decrypt unencrypted credentials data with invalid length")]
        public void Should_Throw_Decrypting_Unencrypted_Credentials_Data_Invalid_Length()
        {
            EncryptedCredentials encryptedCredentials = new EncryptedCredentials
            {
                Data = "dGV4dCBvZiAxNiBjaGFycw==", // unencrypted data
                Hash = "QN3IRzvFR9k/yvDfi9ChnQtIHo8No2SZm3iGwwj4NVk=",
                Secret = "Sr/17/6JrtKCP7X/e9c7XIMAdigeI1QO6u43prhnS9wuNralsZhvPnKIc3qL7A2jcgML273TM2blHywbzt6cAqLxjC" +
                         "ntyjSay0FyMnctarY3soCkCZsUynMsPC9g39CTVBCUXbZZ6tWZ8mgQ9WDXeMVTRaLXLBr9EZdICauFGsln/LaopfU9" +
                         "CvdYXQ0PcdhCFNbisuPwXOqd5jUu0x49+sPAc4V68TsnWRUC3CYEhEfqkRmtomM8UV+/JyHk0zYdiRxarGzAXfgdXJ" +
                         "wjfjXARhERA/hZRYKH+w9vsPpZWdqQg7zSi5EU8Fr2Cs3IzAes+txLUekFprWsKff7j21KXg=="
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptCredentials(encryptedCredentials, EncryptionKey.RsaPrivateKey)
            );

            Assert.Matches(@"^Data hash mismatch at position \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }
    }
}
