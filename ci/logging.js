const chalk = require('chalk');
const $ = require('shelljs')

if (process.env['TRAVIS']) {
    console.info = m => $.echo("\n\033[1;34m#", m, "\033[0m")
    console.debug = m => $.echo("\n\033[0;32m##", m, "\033[0m")
    console.warn = m => $.echo("\n\033[1;33m##", m, "\033[0m")
    console.error = m => $.echo("\n\033[1;31m##", m, "\033[0m")
} else {
    console.info = m => console.log(chalk.blue.bold(`\n# ${m}\n`))
    console.debug = m => console.log(chalk.green.bold(`\n## ${m}\n`))
    console.warn = m => console.log(chalk.yellow.bold(`\n## ${m}\n`))
    console.error = m => console.log(chalk.red.bold(`\n## ${m}\n`))
}
