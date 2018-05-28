﻿using SteamStorefrontAPI;
using SteamStorefrontAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SteamStorefront
{
    /// <summary>
    /// <para type="synopsis">Retrieves information about an application in the steam store.</para>
    /// <para type="description">The Get-SteamApp cmdlet gets information about an application in the</para>
    /// <para type="description">steam store. The returned object contains all information about the</para>
    /// <para type="description">application that's available on the steam storefront api.</para>
    /// <para type="example"></para>
    /// </summary>    
    /// <example>
    ///   <para>Example 1: Get an application by id</para>
    ///   <para></para>
    ///   <code>PS C:\>Get-SteamApp -AppId 323170</code>
    ///   <para></para>
    ///   <para>This command gets steamapp with id 323170.</para>
    ///   <para></para>
    ///   <para>Example 2: Get an application by country and language</para>
    ///   <para></para>
    ///   <code>PS C:\>Get-SteamApp -AppId 323170 -Region "JP" -Language "german"</code>
    ///   <para></para>
    ///   <para>This command gets steamapp with id 323170 with regional parameters like date format,</para>
    ///   <para>price or currency for the japanese steam store, with strings localized to german.</para>
    ///   <para></para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "SteamApp")]
    [OutputType(typeof(SteamApp))]
    public class GetSteamAppCmdlet : Cmdlet
    {
        #region properties
        /// <summary>
        /// <para type="description">Steam Package ID</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("id", "packageid")]
        public int AppId { get; set; }

        /// <summary>
        /// <para type="description">Two letter country code to customise currency and date values.</para>
        /// </summary>
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        [Alias("country", "cc")]
        public string Region { get; set; }

        /// <summary>
        /// <para type="description">Full name of the language in english used for string localization e.g. name, description.</para>
        /// </summary>
        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true)]
        public string Language { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                WriteObject(Task.Run(async () => await AppDetails.GetAsync(AppId, Region, Language)).Result);
            }
            catch (PipelineStoppedException)
            {
                // Nothing to do here, the try block is simply to handle exceptions when the user aborts the command
                return;
            }
            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    WriteError(new ErrorRecord(error, "UnknownError", ErrorCategory.NotSpecified, null));
                }
                return;
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, "UnknownError", ErrorCategory.NotSpecified, null));
                return;
            }
        }

    }
}