# Steam Storefront Powershell Module

A powershell module that uses the [SteamStorefrontAPI library](https://github.com/mmuffins/SteamStorefrontAPI) to retrieve data from the Steam StoreFront API. Also available on [PowerShell Gallery](https://www.powershellgallery.com/packages/SteamStorefront).

## Installation
### Automated
Follow the instructions on [PowerShell Gallery](https://www.powershellgallery.com/packages/SteamStorefront) or run the following command
```powershell
Install-Module -Name SteamStorefront 
```

### Manual
Compile the project and run the following command in the build target directory

```powershell
Import-Module -Name .\SteamStorefront.psd1
Get-Command -Module SteamStorefront
```