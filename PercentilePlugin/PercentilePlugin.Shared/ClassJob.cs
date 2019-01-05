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
    public class ClassJob
    {
        public string Name { get; set; }
        public string Abbrveiation { get; set; }
        public int Key { get; set; }
        
        public static string NameToAbbr(string name)
        {
            switch (name)
            {
                case "Astrologian":
                    return "AST";
                case "Bard":
                    return "BRD";
                case "Black Mage":
                    return "BLM";
                case "Dark Knight":
                    return "DRK";
                case "Dragoon":
                    return "DRG";
                case "Machinist":
                    return "MCH";
                case "Monk":
                    return "MNK";
                case "Ninja":
                    return "NIN";
                case "Paladin":
                    return "PLD";
                case "Scholar":
                    return "SCH";
                case "Summoner":
                    return "SMN";
                case "Warrior":
                    return "WAR";
                case "White Mage":
                    return "WHM";
                case "Red Mage":
                    return "RDM";
                case "Samurai":
                    return "SAM";
                default:
                    return "???";
            }
        }
    }
}
