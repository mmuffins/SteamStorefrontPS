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
    [Cmdlet(VerbsCommon.Get, "SteamPackage")]
    [OutputType(typeof(PackageInfo))]
    public class GetSteamPackageCmdlet : Cmdlet
    {
        #region properties
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("id", "appid")]
        public int PackageId { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        [Alias("country")]
        public string Region { get; set; }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true)]
        public string Language { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(Task.Run(async () => await PackageDetails.GetAsync(PackageId, Region, Language)).Result);
        }

    }
}
