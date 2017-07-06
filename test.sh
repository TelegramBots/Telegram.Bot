#!/usr/bin/env bash

rootdir="`pwd`"
# echo "Working dir => `pwd`"

#exit if any command fails
set -e


echo "## Start test.."

dotnet test "$rootdir/test/Telegram.Bot.Tests.Integ/Telegram.Bot.Tests.Integ.csproj" --configuration Release --list-tests

dotnet test "$rootdir/test/Telegram.Bot.Tests.Integ/Telegram.Bot.Tests.Integ.csproj" --configuration Release  --no-build

echo "#> test completed"
