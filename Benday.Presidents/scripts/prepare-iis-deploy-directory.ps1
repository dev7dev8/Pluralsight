param(
    [Parameter(Mandatory=$true)]
    $buildSourcesDirectory, 
    [Parameter(Mandatory=$true)]
    $toPath)

Write-Output "Preparing deployment directory for supporting files."
Write-Output "buildSourcesDirectory: '$buildSourcesDirectory'"
Write-Output "toPath: '$toPath'"

if(!(Test-Path -Path $toPath)) 
{
    New-Item -ItemType directory -Path $toPath
}


Copy-Item "$buildSourcesDirectory\Benday.Presidents\scripts\EditConnectionString.exe" $toPath
Copy-Item "$buildSourcesDirectory\Benday.Presidents\scripts\deploy-to-iis.ps1" $toPath