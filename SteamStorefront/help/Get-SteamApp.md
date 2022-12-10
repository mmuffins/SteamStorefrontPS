---
external help file: SteamStorefront.dll-Help.xml
Module Name: SteamStorefront
online version:
schema: 2.0.0
---

# Get-SteamApp

## SYNOPSIS
Retrieves information about an application in the steam store.

## SYNTAX

```
Get-SteamApp [-AppId] <Int32> [[-Region] <String>] [[-Language] <String>] [<CommonParameters>]
```

## DESCRIPTION
The Get-SteamApp cmdlet gets information about an application in the steam store. The returned object contains all information about the application that's available on the steam storefront api. 

## EXAMPLES

### Example 1
```powershell
PS C:\>Get-SteamApp -AppId 323170
```

This command gets steamapp with id 323170.

### Example 2
```powershell
PS C:\>Get-SteamApp -AppId 323170 -Region "JP" -Language "german"
```

This command gets steamapp with id 323170 with regional parameters like date format, price or currency for the japanese steam store, with strings localized to german.


## PARAMETERS

### -AppId
Steam Package ID

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: id, packageid

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

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

### SteamStorefrontAPI.Classes.SteamApp

## NOTES

## RELATED LINKS
