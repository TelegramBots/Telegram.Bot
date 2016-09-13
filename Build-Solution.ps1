param(
  [Parameter(Mandatory = $True, Position = 0)]
  [string]$VersionSuffix,
  
  [switch]$Force
)

$rebuild = "";

if ($Force) {
  $rebuild = "--no-incremental"
}

dotnet pack .\src\Telegram.Bot -c Debug --version-suffix $VersionSuffix $rebuild