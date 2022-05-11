## Source generator for Types folder of Telegram.Bot library

This generator parses the https://core.telegram.org/bots/api site and 
generates the JSON output file containing all Types data.

This JSON file will be written exactly to the
`../../../../Telegram.Bot/Types/types.json` file.

`../../../..` will get back from `Telegram.Bot.ApiTypesGenerator/bin/Release/net6.0` folder.

It's enough to run this project in the IDE every time Telegram Bots API
gets a new version of API. No need to run it every build, because it parses the whole site
and can slow down the build.

After this project generates the `types.json` file, the corresponding
source generator will use this file for the next build automatically.
