using System.IO;
using Newtonsoft.Json;

namespace PercentilePlugin
{
    public class Options
    {
        public static Options Instance { get; private set; }

        public string RemoteVersionSeen { get; set; } = "0.0.0";

        public bool AutoUpdate { get; set; } = false;

        public void Save()
        {
            if (Directory.Exists("PercentilePlugin") != true) Directory.CreateDirectory("PercentilePlugin");
            File.WriteAllText("PercentilePlugin/options.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static void Load()
        {
            if (File.Exists("PercentilePlugin/options.json"))
                Instance = JsonConvert.DeserializeObject<Options>(File.ReadAllText("PercentilePlugin/options.json"));
            else
                Instance = new Options();
        }
    }
}