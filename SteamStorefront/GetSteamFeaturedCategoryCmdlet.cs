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
    [Cmdlet(VerbsCommon.Get, "SteamFeaturedCategory")]
    [OutputType(typeof(FeaturedCategory))]
    public class GetSteamFeaturedCategoryCmdlet : Cmdlet
    {
        #region properties
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("country")]
        public string Region { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        public string Language { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            var results = Task.Run(async () => await FeaturedCategories.GetAsync(Region, Language)).Result;
            if(results != null && results.Count > 0)
            {
                results.ForEach(res => WriteObject(res));
            }
        }
    }
}
