const $ = require('shelljs')
const path = require('path')
const fs = require('fs')
require('./logging')

$.config.fatal = true
const root = path.join(__dirname, '..')

const dotnet_image = "microsoft/dotnet:2.0.0-sdk-stretch"

function unit_test(configuration) {
    console.info(`Unit testing on .NET Core`)
    console.debug(`Configuration: ${configuration}`)

    const commands = [
            `cd /app/test/UnitTests/`,
            `dotnet restore`,
            `dotnet build --configuration ${configuration}`,
            `dotnet xunit -configuration ${configuration} -nobuild -verbose`
        ]
        .reduce((prev, curr) => `${prev} && ${curr}`, 'echo')

    $.exec(
        `docker run --rm --tty --volume "${root}:/app/" "${dotnet_image}" ` +
        `sh -c "${commands}"`
    )
}

function generate_app_settings_file() {
    console.info(`Generate appsettings.Development.json from environment variables`)

    const appSettings = {
        'ApiToken': process.env['TelegramBot_ApiToken'],
        'AllowedUserNames': process.env['TelegramBot_AllowedUserNames'],
        'SuperGroupChatId': process.env['TelegramBot_SuperGroupChatId']
    }

    for (const field in appSettings)
        if (!appSettings[field])
            console.warn(`There is no value set for field ${field}.`)

    fs.writeFileSync(`${root}/test/IntegrationTests/appsettings.Development.json`, JSON.stringify(appSettings))
}

function systems_integration_test(configuration) {
    console.info(`Systems Integration testing on .NET Core`)
    console.debug(`Configuration: ${configuration}`)

    const commands = [
            `cd /app/test/IntegrationTests/`,
            `dotnet restore`,
            `dotnet build --configuration ${configuration}`,
            `dotnet xunit -configuration ${configuration} -stoponfail -nobuild -verbose`
        ]
        .reduce((prev, curr) => `${prev} && ${curr}`, 'echo')

    $.exec(
        `docker run --rm --tty --volume "${root}:/app/" "${dotnet_image}" ` +
        `sh -c "${commands}"`
    )
}

console.info(`Docker Image: ${dotnet_image}`)

unit_test('Debug')
unit_test('Release')

const branch = process.env['TRAVIS_BRANCH']
if (branch === 'develop') {
    generate_app_settings_file()
    systems_integration_test('Release')
} else {
    console.warn(`Branch is "${branch}". Skipping systems integration tests...`)
}
