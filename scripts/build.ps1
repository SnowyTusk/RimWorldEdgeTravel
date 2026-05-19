$ErrorActionPreference = 'Stop'

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$modRoot = Split-Path -Parent $scriptRoot
$rimWorldCandidates = @(
  'D:\SteamLibrary\steamapps\common\RimWorld',
  'C:\Program Files (x86)\Steam\steamapps\common\RimWorld',
  'C:\Program Files\Steam\steamapps\common\RimWorld'
)

$rimWorldRoot = $null
foreach ($candidate in $rimWorldCandidates) {
  if (Test-Path (Join-Path $candidate 'RimWorldWin64_Data\Managed\Assembly-CSharp.dll')) {
    $rimWorldRoot = $candidate
    break
  }
}

if (-not $rimWorldRoot) {
  throw 'Could not locate RimWorld installation.'
}

$managed = Join-Path $rimWorldRoot 'RimWorldWin64_Data\Managed'
$sourceFiles = Get-ChildItem -Path (Join-Path $modRoot 'Source') -Filter *.cs | Sort-Object FullName | Select-Object -ExpandProperty FullName
$assemblies = Join-Path $modRoot '1.6\Assemblies'
$output = Join-Path $assemblies 'EdgeTravel.dll'
$csc = "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
$steamapps = Split-Path -Parent (Split-Path -Parent $rimWorldRoot)
$harmony = Join-Path $steamapps 'workshop\content\294100\2009463077\Current\Assemblies\0Harmony.dll'

if (-not (Test-Path $harmony)) {
  throw "Could not locate Harmony at $harmony"
}

New-Item -ItemType Directory -Force -Path $assemblies | Out-Null

& $csc `
  /nologo `
  /target:library `
  /langversion:5 `
  /out:$output `
  /reference:"$managed\Assembly-CSharp.dll" `
  /reference:"$managed\netstandard.dll" `
  /reference:"$managed\UnityEngine.dll" `
  /reference:"$managed\UnityEngine.CoreModule.dll" `
  /reference:"$managed\Unity.Mathematics.dll" `
  /reference:"$harmony" `
  $sourceFiles

if ($LASTEXITCODE -ne 0) {
  throw "csc failed with exit code $LASTEXITCODE"
}

Write-Host "Built $output"
