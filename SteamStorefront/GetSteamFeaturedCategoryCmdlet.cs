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
    /// <para type="synopsis">Retrieves a list of all featured app categories and applications in these categories.</para>
    /// <para type="description">The Get-SteamFeaturedCategory cmdlet gets a list of all featured categories on 
    /// steam front page, e.g. new releases, top sellers, spotlights or daily deals and lists 
    /// all applications in each of the categories.</para>
    /// <para type="example"></para>
    /// </summary>    
    /// <example>
    ///   <para>Example 1: Get a list of all featured app categories by region</para>
    ///   <para></para>
    ///   <code>PS C:\>Get-SteamFeaturedCategory -Region "US"</code>
    ///   <para>This command gets a list of all featured app categories for region US.</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "SteamFeaturedCategory")]
    [OutputType(typeof(FeaturedCategory))]
    public class GetSteamFeaturedCategoryCmdlet : Cmdlet
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

        /// <summary>
        /// <para type="description">ProcessRecord method</para>
        /// </summary>
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            var results = Task.Run(async () => await FeaturedCategories.GetAsync(Region, Language)).Result.ToList();
            if(results != null && results.Count > 0)
            {
                results.ForEach(res => WriteObject(res));
            }
        }
    }
}
