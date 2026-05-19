$ErrorActionPreference = 'Stop'

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$repoRoot = Split-Path -Parent $scriptRoot
$distRoot = Join-Path $repoRoot 'dist'
$modName = 'EdgeTravel'
$publishRoot = Join-Path $distRoot $modName
$rimWorldModsRoot = 'D:\SteamLibrary\steamapps\common\RimWorld\Mods'
$gameModRoot = Join-Path $rimWorldModsRoot $modName

& (Join-Path $scriptRoot 'build.ps1')

$runtimeFolders = @(
  'About',
  'Defs',
  '1.6'
)

function Copy-RuntimeMod {
  param(
    [Parameter(Mandatory = $true)]
    [string] $TargetRoot
  )

  if (Test-Path $TargetRoot) {
    Remove-Item -LiteralPath $TargetRoot -Recurse -Force
  }

  New-Item -ItemType Directory -Force -Path $TargetRoot | Out-Null

  foreach ($folder in $runtimeFolders) {
    $source = Join-Path $repoRoot $folder
    $target = Join-Path $TargetRoot $folder
    Copy-Item -LiteralPath $source -Destination $target -Recurse -Force
  }

  Assert-RuntimeOnly $TargetRoot
}

function Assert-RuntimeOnly {
  param(
    [Parameter(Mandatory = $true)]
    [string] $TargetRoot
  )

  $unexpectedFiles = Get-ChildItem -LiteralPath $TargetRoot -Recurse -Force |
    Where-Object {
      $_.FullName -match '\\\.git(\\|$)' -or
      $_.FullName -match '\\Source(\\|$)' -or
      $_.Name -in @('build.ps1', 'publish.ps1', 'README.md', 'WORKSHOP.md', 'CHANGELOG.md', 'RELEASE_NOTES.md', 'LICENSE', '.gitignore')
    }

  if ($unexpectedFiles) {
    $unexpectedList = $unexpectedFiles | Select-Object -ExpandProperty FullName
    throw "Publish folder contains development-only files:`n$($unexpectedList -join "`n")"
  }
}

Copy-RuntimeMod $publishRoot

if (Test-Path $rimWorldModsRoot) {
  Copy-RuntimeMod $gameModRoot
  Write-Host "Copied runtime mod to $gameModRoot"
} else {
  Write-Warning "RimWorld Mods folder not found: $rimWorldModsRoot"
}

Write-Host "Prepared Workshop package at $publishRoot"
