# Telegram.Bot NativeAOT demo

This sample shows the `Telegram.Bot` client running as a self-contained NativeAOT binary.

## How to run

- Export your bot token: `set TELEGRAM_BOT_TOKEN=123456:ABC...` (PowerShell) or `export TELEGRAM_BOT_TOKEN=...` (bash).
- Publish AOT for your platform, e.g.: `dotnet publish -c Release -r win-x64 -p:PublishAot=true`.
- Run the published app from `bin/Release/net8.0/win-x64/native/NativeAot.exe` and send the bot any message to see it echoed back.
