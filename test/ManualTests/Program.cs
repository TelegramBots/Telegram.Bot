// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

Console.WriteLine("Hello, World!");
// {\"type\":\"emoji\",\"emoji\":\"\\ud83d\\udc4d\"}

string json_emoji = "{\"type\":\"emoji\",\"emoji\":\"👍\"}";
string json_custom_emoji = "{\"type\":\"custom_emoji\",\"custom_emoji_id\":\"12345\"}";

var reaction_emoji = JsonSerializer.Deserialize<ReactionType>(json_emoji);
var reaction_custom_emoji = JsonSerializer.Deserialize<ReactionType>(json_custom_emoji);

string t1 = JsonSerializer.Serialize(reaction_emoji);
string t2 = JsonSerializer.Serialize(reaction_custom_emoji);

Console.WriteLine(reaction_emoji);
