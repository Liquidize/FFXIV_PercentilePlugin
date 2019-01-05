using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PercentilePlugin.Shared
{
    public class Encounter
    {
        public string Name { get; set; }
        public string LastUpdated { get; set; }
        public int Key { get; set; }
        public int Category { get; set; }
    }
}
