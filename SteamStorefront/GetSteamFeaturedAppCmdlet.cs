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
    [Cmdlet(VerbsCommon.Get, "SteamFeaturedApp")]
    [OutputType(typeof(FeaturedApps))]
    public class GetSteamFeaturedAppCmdlet : Cmdlet
    {
        #region properties
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("country", "cc")]
        public string Region { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        public string Language { get; set; }

        #endregion

        /// <summary>
        /// <para type="description">BeginProcessing method</para>
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        /// <summary>
        /// <para type="description">ProcessRecord method</para>
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                WriteObject(Task.Run(async () => await Featured.GetAsync(Region, Language)).Result);
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
        }

        /// <summary>
        /// <para type="description">StopProcessing method</para>
        /// </summary>
        protected override void StopProcessing()
        {
            base.StopProcessing();
        }

    }
}
