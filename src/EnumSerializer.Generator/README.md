# Enum Converter Generator

## Background

Telegram.Bot library relies on [Json.NET converters](https://www.newtonsoft.com/json/help/html/CustomJsonConverter.htm)
to map JSON input to various enums and vice versa.

It's rather tedious and repeating task. So that's where C# source generators come to help.

`EnumSerializer.Generator` looks for enums
annotated with `[JsonConverter(typeof(TEnumConverter))]` attribute and generates a converter that handles all possible enum values for us.

## Credits

This project is heavily inspired by the series of posts by Andrew Lock [Creating an incremental generator](https://andrewlock.net/creating-a-source-generator-part-1-creating-an-incremental-source-generator/) and [NetEscapades.EnumGenerators
](https://github.com/andrewlock/NetEscapades.EnumGenerators) project.

We use Alexandre Mutel's [Scriban](https://github.com/scriban/scriban) templating engine to generate
converter class output.
