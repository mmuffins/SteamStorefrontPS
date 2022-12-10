---
external help file: SteamStorefront.dll-Help.xml
Module Name: SteamStorefront
online version:
schema: 2.0.0
---

# Get-SteamPackage

## SYNOPSIS
Retrieves information about a package in the steam store.

## SYNTAX

```
Get-SteamPackage [-PackageId] <Int32> [[-Region] <String>] [[-Language] <String>] [<CommonParameters>]
```

## DESCRIPTION
The Get-SteamPackage cmdlet gets information about a package in the steam store. The returned object contains all information about the package that's available on the steam storefront api.

## EXAMPLES

### Example 1
```powershell
PS C:\>Get-SteamPackage -PackageId 73194
```

This command gets package with id 73194.

### Example 2
```powershell
PS C:\>Get-SteamPackage -PackageId 73194 -Region "AT" -Language "spanish"
```

This command gets package with id 73194 with regional parameters like date format, price or currency for the austrian steam store, with strings localized to spanish.

## PARAMETERS

### -Language
Full name of the language in english used for string localization e.g. name, description.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -PackageId
Steam App ID

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: id, appid

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Region
Two letter country code to customise currency and date values.

```yaml
Type: String
Parameter Sets: (All)
Aliases: country, cc

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Int32

### System.String

## OUTPUTS

### SteamStorefrontAPI.Classes.PackageInfo

## NOTES

## RELATED LINKS
