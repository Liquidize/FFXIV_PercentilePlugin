using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace PercentilePlugin.Shared
{
    public class PercentileData
    {
        public long LastUpdated { get; set; } = 0;

        public Dictionary<string, Dictionary<string, List<double>>> Rankings { get; set; } =
            new Dictionary<string, Dictionary<string, List<double>>>();
        
        public static PercentileData Load(string file)
        {
            // Return new instance if file does not exists
            if (File.Exists(file) != true) return new PercentileData();

            var fileStream = new FileStream(file, FileMode.OpenOrCreate);
            using (var reader = new BsonReader(fileStream))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<PercentileData>(reader);
            }
        }
    }
}