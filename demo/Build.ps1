param([Parameter(Mandatory=$true)][string]$Output)
$ErrorActionPreference = 'Stop'
$root = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).ProviderPath
$target = [IO.Path]::GetFullPath($Output)
if ($target -eq $root -or $target.StartsWith($root + [IO.Path]::DirectorySeparatorChar,[StringComparison]::OrdinalIgnoreCase)) {
  throw 'Choose a build folder outside the repository.'
}
if (Test-Path -LiteralPath $target) { throw 'Choose a new output folder.' }
$site = Join-Path $target 'site'
$null = New-Item -ItemType Directory -Path $site
Get-ChildItem -LiteralPath $root | Where-Object { $_.Name -notin @('demo','docs','README.md','LICENSE','THIRD-PARTY-NOTICES.md','Web.config') } |
  Copy-Item -Destination $site -Recurse
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'Web.config.example') -Destination (Join-Path $site 'Web.config')
$compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/aspnet_compiler.exe'
& $compiler -v / -p $site -f (Join-Path $target 'precompiled')
if ($LASTEXITCODE -ne 0) { throw "ASP.NET compilation failed ($LASTEXITCODE)." }
