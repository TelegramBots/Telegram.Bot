using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendContactMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SendingContactMessageTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should send a contact")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendContact)]
    public async Task Should_Send_Contact()
    {
        const string phoneNumber = "+1234567890";
        const string firstName = "Han";
        const string lastName = "Solo";

        Message message = await BotClient.SendContact(
            chatId: Fixture.SupergroupChat,
            phoneNumber: phoneNumber,
            firstName: firstName,
            lastName: lastName
        );

        Assert.Equal(MessageType.Contact, message.Type);
        Assert.Equal(phoneNumber, message.Contact!.PhoneNumber);
        Assert.Equal(firstName, message.Contact.FirstName);
        Assert.Equal(lastName, message.Contact.LastName);
    }

    [OrderedFact("Should send a contact including his vCard")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendContact)]
    public async Task Should_Send_Contact_With_VCard()
    {
        string vcard = """
            BEGIN:VCARD
            VERSION:2.1
            N:Gump;Forrest;;Mr.
            FN:Forrest Gump
            ORG:Bubba Gump Shrimp Co.
            TITLE:Shrimp Man
            PHOTO;JPEG:https://upload.wikimedia.org/wikipedia/commons/9/95/TomHanksForrestGump94.jpg
            TEL;WORK;VOICE:(111) 555-1212
            TEL;HOME;VOICE:(404) 555-1212
            ADR;HOME:;;42 Plantation St.;Baytown;LA;30314;United States of America
            LABEL;HOME;ENCODING=QUOTED-PRINTABLE;CHARSET=UTF-8:42 Plantation St.=0D=0A=
             Baytown, LA 30314=0D=0AUnited States of America
            EMAIL:forrestgump@example.org
            REV:20080424T195243Z
            END:VCARD
            """;

        Message message = await BotClient.SendContact(
            chatId: Fixture.SupergroupChat,
            phoneNumber: "+11115551212",
            firstName: "Forrest",
            vcard: vcard
        );

        Assert.Equal(MessageType.Contact, message.Type);
        Assert.NotNull(message.Contact);
        Assert.Equal(vcard.Replace("\r", ""), message.Contact.Vcard);
    }
}
