using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class EncryptedPassportElementTypeConverterTests
{
    [Theory]
    [ClassData(typeof(EncryptedPassportElementData))]
    public void Should_Convert_EncryptedPassportElementType_To_String(EncryptedPassportElement encryptedPassportElement, string value)
    {

        string result = JsonSerializer.Serialize(encryptedPassportElement, TelegramBotClientJsonSerializerContext.Instance.EncryptedPassportElement);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(EncryptedPassportElementData))]
    public void Should_Convert_String_To_EncryptedPassportElementType(EncryptedPassportElement expectedResult, string value)
    {
        EncryptedPassportElement? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.EncryptedPassportElement);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_EncryptedPassportElementType()
    {
        EncryptedPassportElementType? result =
            JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.EncryptedPassportElementType);

        Assert.NotNull(result);
        Assert.Equal((EncryptedPassportElementType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_EncryptedPassportElementType()
    {
        // ToDo: add EncryptedPassportElementType.Unknown ?
        //    protected override string GetStringValue(EncryptedPassportElementType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((EncryptedPassportElementType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.EncryptedPassportElementType));
    }

    private class EncryptedPassportElementData : IEnumerable<object[]>
    {
        private static EncryptedPassportElement NewEncryptedPassportElement(EncryptedPassportElementType encryptedPassportElementType)
        {
            return new EncryptedPassportElement
            {
                Type = encryptedPassportElementType,
                Hash = "hash",
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.PersonalDetails), """{"type":"personal_details","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.Passport), """{"type":"passport","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.DriverLicence), """{"type":"driver_licence","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.IdentityCard), """{"type":"identity_card","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.InternalPassport), """{"type":"internal_passport","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.Address), """{"type":"address","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.UtilityBill), """{"type":"utility_bill","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.BankStatement), """{"type":"bank_statement","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.RentalAgreement), """{"type":"rental_agreement","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.PassportRegistration), """{"type":"passport_registration","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.TemporaryRegistration), """{"type":"temporary_registration","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.PhoneNumber), """{"type":"phone_number","hash":"hash"}"""];
            yield return [NewEncryptedPassportElement(EncryptedPassportElementType.Email), """{"type":"email","hash":"hash"}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
