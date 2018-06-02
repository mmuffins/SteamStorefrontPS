param (
[string]$Psd1Path,
[string]$ProjectPath,
[string]$AssemblyInfo,
[string]$TargetFileName,
[string]$Version
)

function Get-AssemblyInformation(){
    param (
        [object]$AssemblyObject,
        [string]$PropertyName
    )

    $matches = $null
    $AssemblyObject -match "assembly: $PropertyName\(`"(.*?)`"\)\]" | Out-Null
    return $matches[1]
}

Write-Output "Generating module manifest"

if(!(Test-Path $Psd1Path))
{
    Write-Error "Could not find file $Psd1Path."
    exit 
}

$psd1 = Get-Content -Path $Psd1Path -Encoding UTF8
if($psd1 -eq $null)
{
    Write-Error "Could not read file $Psd1Path ."
    exit 
}

if(!(Test-Path $ProjectPath))
{
    Write-Error "Could not find file $ProjectPath."
    exit 
}

[xml]$project = Get-Content -Path $ProjectPath
if($project -eq $null)
{
    Write-Error "Could not read file $ProjectPath ."
    exit 
}

if(!(Test-Path $AssemblyInfo))
{
    Write-Error "Could not find file $AssemblyInfo ."
    exit 
}

$assemblyInfo = Get-Content -Path $AssemblyInfo 
if($assemblyInfo -eq $null)
{
    Write-Error "Could not read file $AssemblyInfo ."
    exit 
}

$psd1 = $psd1 -replace '%date%', ((Get-Date).tostring("dd/MM/yyyy"))
$psd1 = $psd1 -replace '%targetfilename%', $TargetFileName
$psd1 = $psd1 -replace '%fileversion%', $Version

$assemblyReplace = @(
    @{Property='Guid'; Variable='%guid%'}
    ,@{Property='AssemblyTitle'; Variable='%title%'}
    ,@{Property='AssemblyCompany'; Variable='%company%'}
    ,@{Property='AssemblyDescription'; Variable='%description%'}
    ,@{Property='AssemblyCopyright'; Variable='%copyright%'}
    ,@{Property='AssemblyFileVersion'; Variable='%fileversion%'}
)

foreach ($item in $assemblyReplace) {
    $newProp = Get-AssemblyInformation -AssemblyObject $AssemblyInfo -PropertyName $item.Property
    if($item.Property -eq "AssemblyCopyright"){
        $newProp = $newProp -replace [char]0x00A9, "(c)"
    }
    $psd1 = $psd1 -replace $item.Variable, $newProp

}

$psd1 | Out-File $Psd1Path -Force