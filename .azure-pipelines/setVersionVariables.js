const assert = require('assert').strict;

const prefix = process.env['VERSION_PREFIX'];
const suffix = process.env['VERSION_SUFFIX'];
const ciSuffix = process.env['CI_VERSION_SUFFIX'];

const prefixRegex = /[0-9]+\.[0-9]+\.[0-9]+/;
const ciSuffixRegex = /ci\.[0-9]+\+git\.commit\.[0-9a-z]{40}/;

assert.ok(prefix, "Version prefix is empty");
assert.ok(ciSuffix, "CI version suffix is empty");
assert.ok(prefixRegex.test(prefix), "Version prefix is invalid");
assert.ok(ciSuffixRegex.test(ciSuffix), "CI version prefix is invalid");

const releaseVersionCommand = createCommand(
    'RELEASE_VERSION',
    suffix && suffix.length > 0
    ? `${prefix}-${suffix}`
    : `${prefix}`
);

const ciVersionCommand = createCommand(
    'CI_VERSION',
    suffix && suffix.length > 0
    ? `${prefix}-${suffix}.${ciSuffix}`
    : `${prefix}-${ciSuffix}`
);

const isPreRelease = createCommand(
    'IS_PRE_RELEASE',
    suffix && suffix.length > 0 ? true : false
);

console.log(releaseVersionCommand);
console.log(ciVersionCommand);
console.log(isPreRelease);

function createCommand(key, value) {
    return `##vso[task.setvariable variable=${key};]${value}`
}
