using SteamStorefrontAPI;
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
    /// <para type="synopsis">Retrieves a list of all apps on the steam store.</para>
    /// <para type="description">The Get-SteamFeaturedApp cmdlet gets a list of all featured categories on </para>
    /// <para type="description">steam front page, grouped by platforms.</para>
    /// <para type="example"></para>
    /// </summary>    
    /// <example>
    ///   <para>Example 1: Get a list of all featured apps by region.</para>
    ///   <para></para>
    ///   <code>PS C:\>Get-SteamFeaturedApp -Region "US"</code>
    ///   <para></para>
    ///   <para>This command gets a list of all featured apps for region US.</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "SteamFeaturedApp")]
    [OutputType(typeof(FeaturedApps))]
    public class GetSteamFeaturedAppCmdlet : Cmdlet
    {
        #region properties
        /// <summary>
        /// <para type="description">Two letter country code to customise currency and date values.</para>
        /// </summary>
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("country", "cc")]
        public string Region { get; set; }

        /// <summary>
        /// <para type="description">Full name of the language in english used for string localization e.g. name, description.</para>
        /// </summary>
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        public string Language { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(Task.Run(async () => await Featured.GetAsync(Region, Language)).Result);
        }

    }
}
