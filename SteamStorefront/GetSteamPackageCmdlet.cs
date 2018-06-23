using SteamStorefrontAPI;
using SteamStorefrontAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SteamStorefront
{

    /// <summary>
    /// <para type="synopsis">Retrieves information about a package in the steam store.</para>
    /// <para type="description">The Get-SteamPackage cmdlet gets information about a package in the
    /// steam store. The returned object contains all information about the
    /// package that's available on the steam storefront api.</para>
    /// <para type="example"></para>
    /// </summary>    
    /// <example>
    ///   <para>Example 1: Get an application by id</para>
    ///   <para></para>
    ///   <code>PS C:\>Get-SteamPackage -PackageId 73194</code>
    ///   <para>This command gets package with id 73194.</para>
    ///   <para></para>
    ///   <para>Example 2: Get an application by country and language</para>
    ///   <para></para>
    ///   <code>PS C:\>Get-SteamPackage -PackageId 73194 -Region "AT" -Language "spanish"</code>
    ///   <para>This command gets package with id 73194 with regional parameters like date format,
    ///   price or currency for the austrian steam store, with strings localized to spanish.</para>
    ///   <para></para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "SteamPackage")]
    [OutputType(typeof(PackageInfo))]
    public class GetSteamPackageCmdlet : Cmdlet
    {
        #region properties
        /// <summary>
        /// <para type="description">Steam App ID</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("id", "appid")]
        public int PackageId { get; set; }

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

        private SecurityProtocolType originalSecurityProtocol;
        #endregion

        /// <summary>
        /// <para type="description">BeginProcessing method</para>
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            // It's not possible to set the security protocol per request, store the current configuration,
            // update it to the values needed in the current call and restore it to its original setting 
            originalSecurityProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// <para type="description">ProcessRecord method</para>
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                WriteObject(Task.Run(async () => await PackageDetails.GetAsync(PackageId, Region, Language)).Result);
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

        /// <summary>
        /// <para type="description">EndProcessing method</para>
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
            ServicePointManager.SecurityProtocol = originalSecurityProtocol;
        }

        /// <summary>
        /// <para type="description">StopProcessing method</para>
        /// </summary>
        protected override void StopProcessing()
        {
            base.StopProcessing();
            ServicePointManager.SecurityProtocol = originalSecurityProtocol;
        }
    }
}
