---
external help file: SteamStorefront.dll-Help.xml
Module Name: SteamStorefront
online version:
schema: 2.0.0
---

# Get-SteamFeaturedCategory

## SYNOPSIS
Retrieves a list of all featured app categories and applications in these categories.

## SYNTAX

```
Get-SteamFeaturedCategory [[-Region] <String>] [[-Language] <String>] [<CommonParameters>]
```

## DESCRIPTION
The Get-SteamFeaturedCategory cmdlet gets a list of all featured categories on steam front page, e.g. new releases, top sellers, spotlights or daily deals and lists all applications in each of the categories.

## EXAMPLES

### Example 1
```powershell
PS C:\>Get-SteamFeaturedCategory -Region "US"
```

This command gets a list of all featured app categories for region US.

## PARAMETERS

### -Language
Full name of the language in english used for string localization e.g. name, description.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
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
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### SteamStorefrontAPI.Classes.FeaturedCategory

## NOTES

## RELATED LINKS
