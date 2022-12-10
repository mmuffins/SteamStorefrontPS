Param
(
    [string]$modulePath,
    [string]$moduleVersion,
    [string]$moduleOwner,
    [string]$moduleDescription,
    [string]$moduleCopyright,
    [string]$targetPath
)

$moduleRequireLicense = $false

if(-not (Test-Path -Path $modulePath))
{
    Write-Error "Could not find $modulePath!"
    return
}

## Update the file here
Write-Host "Updating the module manifest file."

Update-ModuleManifest -Path $modulePath -Author $moduleOwner -Copyright $moduleCopyright -Description $moduleDescription -ModuleVersion $moduleVersion -RootModule $targetPath

Write-Host "Validating the powershell module manifest."

try
{
    Test-ModuleManifest -Path $modulePath -ErrorAction Stop
}
catch 
{
    $ErrorMessage = $_.Exception.Message
    Write-Error "$ErrorMessage"
    return
}

Write-Host "The module manifest is valid."