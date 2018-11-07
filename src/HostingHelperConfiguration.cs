using System;
using Serilog;

namespace Microsoft.AspNetCore.Hosting
{

    public class HostingHelperConfiguration
    {
        public System.Collections.Generic.List<JsonFile> ConfigFiles { get; set; } = new System.Collections.Generic.List<JsonFile>(){};
    }
}