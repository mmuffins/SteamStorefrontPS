﻿using SteamStorefrontAPI;
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
    [Cmdlet(VerbsCommon.Get, "SteamFeaturedCategory")]
    [OutputType(typeof(FeaturedCategory))]
    public class GetSteamFeaturedCategoryCmdlet : Cmdlet
    {
        #region properties
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("country", "cc")]
        public string Region { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
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
                var results = Task.Run(async () => await FeaturedCategories.GetAsync(Region, Language)).Result.ToList();
                if (results != null && results.Count > 0)
                {
                    results.ForEach(res => WriteObject(res));
                }
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
