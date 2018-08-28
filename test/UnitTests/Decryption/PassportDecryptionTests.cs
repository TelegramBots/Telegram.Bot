// ReSharper disable PossibleNullReferenceException

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Decryption
{
    /// <summary>
    /// Tests for decryption of passport_data received for authorization request with the scope of "passport".
    /// Test data has 2 passport elements: Personal Details, Passport(document data and main page JPG file)
    /// </summary>
    public class PassportDecryptionTests
    {
        private readonly ITestOutputHelper _output;

        public PassportDecryptionTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "Should decrypt 'passport_data.credentials'")]
        public void Should_decrypt_credentials()
        {
            RSA key = EncryptionKey.GetRsaPrivateKey();
            PassportData passData = GetPassportData();

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(encryptedCredentials: passData.Credentials);

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.NotEmpty(credentials.Payload);
            Assert.Equal("TEST", credentials.Payload);

            // 'personal_details' element decryption needs accompanying DataCredentials
            Assert.NotNull(credentials.SecureData.PersonalDetails);
            Assert.NotNull(credentials.SecureData.PersonalDetails.Data);
            Assert.NotEmpty(credentials.SecureData.PersonalDetails.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.PersonalDetails.Data.DataHash);

            Assert.NotNull(credentials.SecureData.Passport);
            // docuemnt data in 'passport' element decryption needs accompanying DataCredentials:
            Assert.NotNull(credentials.SecureData.Passport.Data);
            Assert.NotEmpty(credentials.SecureData.Passport.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.Passport.Data.DataHash);
            // front side of 'passport' element decryption needs accompanying FileCredentials:
            Assert.NotNull(credentials.SecureData.Passport.FrontSide);
            Assert.NotEmpty(credentials.SecureData.Passport.FrontSide.Secret);
            Assert.NotEmpty(credentials.SecureData.Passport.FrontSide.FileHash);
        }

        [Fact(DisplayName = "Should decrypt 'personal_details' element")]
        public void Should_decreypt_personal_details_element()
        {
            RSA key = EncryptionKey.GetRsaPrivateKey();
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            EncryptedPassportElement persDetEl = Assert.Single(passportData.Data, el => el.Type == "personal_details");

            PersonalDetails personalDetails = decrypter.DecryptData<PersonalDetails>(
                encryptedData: persDetEl.Data,
                dataCredentials: credentials.SecureData.PersonalDetails.Data
            );

            Assert.Equal("John", personalDetails.FirstName);
            Assert.Equal("Smith", personalDetails.LastName);
            Assert.Equal(PassportEnums.Gender.Male, personalDetails.Gender);
            Assert.Equal("US", personalDetails.CountryCode); // U.S.A
            Assert.Equal("ES", personalDetails.ResidenceCountryCode); // Spain
            Assert.Equal("01.01.2018", personalDetails.BirthDate);

            DateTime birthDate = DateTime.ParseExact(personalDetails.BirthDate, "dd.MM.yyyy", null);
            Assert.Equal(new DateTime(2018, 1, 1), birthDate);
        }

        [Fact(DisplayName = "Should decrypt docuemnt data in 'passport' element")]
        public void Should_decreypt_passport_element_document()
        {
            RSA key = EncryptionKey.GetRsaPrivateKey();
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            EncryptedPassportElement passportEl = Assert.Single(passportData.Data, el => el.Type == "passport");

            IdDocumentData documentData = decrypter.DecryptData<IdDocumentData>(
                passportEl.Data,
                credentials.SecureData.Passport.Data
            );

            Assert.Equal("ABCD1234", documentData.DocumentNo);
            Assert.Empty(documentData.ExpiryDate);
            Assert.Null(documentData.Expiry);
        }

        [Fact(DisplayName = "Should decrypt front side photo in 'passport' element")]
        public async Task Should_decreypt_passport_element_frontside()
        {
            RSA key = EncryptionKey.GetRsaPrivateKey();
            PassportData passportData = GetPassportData();
            EncryptedPassportElement passportEl = Assert.Single(passportData.Data, el => el.Type == "passport");

            Assert.NotNull(passportEl.FrontSide);
            Assert.Equal("DgADAQADLwADrT9JRxbZ8eqGF-jJAg", passportEl.FrontSide.FileId);
            Assert.InRange(passportEl.FrontSide.FileDate, new DateTime(2018, 8, 6), new DateTime(2018, 8, 7));

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encryptedContent = await System.IO.File.ReadAllBytesAsync("Files/passport-front_side.jpg.enc");
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.Passport.FrontSide
            );

            Assert.NotNull(content);
            Assert.NotEmpty(content);

            string tmpFile = System.IO.Path.GetTempFileName();
            await System.IO.File.WriteAllBytesAsync(tmpFile, content);
            _output.WriteLine($"Saved JPG photo to file \"{tmpFile}\".");
        }

        static PassportData GetPassportData() =>
            JsonConvert.DeserializeObject<PassportData>(@"
            {
              ""data"": [
                {
                  ""type"": ""personal_details"",
                  ""data"": ""9UemjYkbmwSylfhHUejiTOFiy5YTzbmgyu4NaMkKQVMffk2QouaECOjKawqANVkPKyPWXoKz3phw/+IvpInw8DJwJ0ah2WE/rZO5PTVSlLEnf/nNl8MXgLxzTSWp5ZrBh1YrOr0Hhk+V4m7EXouCbeTdt/J++KC7jP1AiClsCYq7xLkKZUT2jW+hg11atKCtu9FAwcxRaBqW4RG3AjKemOHvpaV24f8odbDphCnGrl0utBcswgU0jEEI2gROBym/iX5ysLtvvPYDgBF/gBPX/fNnZ37yyUlFHJPBy7qIToSaWqy0OjoEOoxUarilHIPQgbISpfYCLgZn3YHmca9NU+xwK2f3HymcfYGYj9TiTvYHXQLJ+qAuMyqJ3HK7j/I16fX824R9Z8plh5afJfIL+bD9tC4oV4rd8Q+fLThzP4aaxd35MPqiliJF3qCDiy9a"",
                  ""hash"": ""XFCkKZSxKuOieT5Z6Pp8YFCNZoMzaD0mdAOc7E0hNZA=""
                },
                {
                  ""type"": ""passport"",
                  ""data"": ""ns7KLY+rQXUMWhdHFl4mHalzidd7yGY7FP0lKP9JFp3k+kzbqlivOgXkzbYn2mms62jGuwv2uFXhsfaWXNUe/ALvKshyTDP+LvCVR1+u810JyOqfDbZumNfjXpRh9ZCiq+k1DnFuENjin88GLl8AWQ=="",
                  ""front_side"": {
                    ""file_id"": ""DgADAQADLwADrT9JRxbZ8eqGF-jJAg"",
                    ""file_date"": 1533576088
                  },
                  ""hash"": ""9oOFR85356pKRnriDX74/bqHA7dihfdxELIHqP4/6/k=""
                }
              ],
              ""credentials"": {
                ""data"": ""aClHU3COACNlD/fwR3IOxD2FQBUyxVGN6wwKomUAp76yr0Mm2j/Vy/yNj8Y0AVUbfgUOP8vEm8mDRdrygPgg1fPQ7tbV2PMFCshtRlq/oKIa0YTCPG61IAjlDHCEOmadZGTRq2QILbGGyG1amLT5hAqj6viCHDK82pIFl8pYzDfw0IaBDwTJWmICWBMkTXPB13HFABejcdyqV5k++3vQP3c9eRgOg7MnJKIufekHzK2P99wL96RZuucK6OVkUk43XYnu+CtX6165G9tHT0IEKfW2ilOXp8lNLKrTT1eMIBjsrwrbx6QwtXe/Ua3Hs7fclSjnRoYYp/3pO9N9fs7rvdwLpN3qy8wQfO5mT83mTGCYERBV1oivqTzUegH2Q/KnnQPCrJ+bQaC3BW2inWhLX1yFx8R88cjFs4hORMX2tnGzvMUNSJHP2m5sJyYPAq1Lp0oGyNMFx7krJqJElFjTL+vjrqhEYxOJ5BXHXalShJ/0EXk4TC2U0W/WbuyoKYUW2+XB3XA77QZfMP38WJdNOv/iaWnyDyvx1N9qY/yvZkxcvtYnro6926nOfbp6dA5rV7vnzek8TROrA7+WuLtACmx8UEDa4SKRDSRMfOWurYJXxzOhN5geI1zmvvXl1hcm6ks6Laz2fdl4Nk+UagJiNhLCN2SEXNJYHL511MmTArzS9gR7uc+DWsZ9/aTwyF8I6kop7hUedcNrFmXFqCrs7z3wemeT/fZtPwHL4zVqzaqPVg3oyf6+tVBIJgmfEL1KsKPqVElC7+vpdk6YIoe4BhTfXuZ/0xXzjHIWATvTb4IFWJV78jaDFhORniJyRyTNnC7nSDxGuqnYhNPM7Fw1dA=="",
                ""hash"": ""myQgdwd3+nVloSSSylKggfAGuEqoNiHRFjoRty+vG18="",
                ""secret"": ""dIGXv1zEvocs4qj5jqUqbYZQhr7MFcoDXxOb8EEQOn4mNVt21AR1CJsOw1yacLNLNfXnZ75/m7eLvuEX2gkQkBH9h4sY9MhBPdx9H+KcPfTY4PxAx865EnjBugyJc4lLwvZtd1ylK6hp3U8p8cwrhYnZaeJEwYsNNnY1KW9BID07FuOOOFRG7IOMKGTUAfyZ+/UmBP3u9qspHlShvZ+/o9C0DlgrEOFtAub+SP+WHnoW17p0+3Va1RvuKR9RpTOkNMeVjrvBrCKOU0nxc6QB1cFEKLzxFBcklKEJjntO0Zn1Zckf4+B81gdi0NK+hj18+zjnU+GvhFYF5klEMfxBUw==""
              }
            }");
    }
}
