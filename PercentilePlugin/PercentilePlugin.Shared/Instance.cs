using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PercentilePlugin.Shared
{
   public class Instance
    {
        public Dictionary<string, Encounter> Encounters { get; set; } = new Dictionary<string, Encounter>();

        public string MapName { get; set; } = "";
        
        public static string InstanceFromBoss(string boss)
        {
            switch (boss)
            {
                case "Famfrit, the Darkening Cloud":
                case "Belias, the Gigas":
                case "Construct 7":
                case "Yiazmat":
                    return "The Ridorana Lighthouse";
                    break;
                case "Mateus, the Corrupt":
                case "Hashmal, Bringer of Order":
                case "Rofocale":
                case "Argath Thadalfus":
                    return "The Royal City of Rabanastre";
                    break;
                case "Phantom Train (Savage)":
                    return "Sigmascape V1.0 (Savage)";
                    break;
                case "Demon Chadarnook (Savage)":
                    return "Sigmascape V2.0 (Savage)";
                    break;
                case "Guardian (Savage)":
                    return "Sigmascape V3.0 (Savage)";
                    break;
                case "Kefka (Savage)":
                case "God Kefka (Savage)":
                    return "Sigmascape V4.0 (Savage)";
                break;
                case "Phantom Train":
                    return "Sigmascape (V1.0)";
                    break;
                case "Demon Chadarnook":
                    return "Sigmascape (V2.0)";
                    break;
                case "Guardian":
                    return "Sigmascape (V3.0)";
                    break;
                case "Kefka":
                    return "Sigmascape (V4.0)";
                    break;
                case "Susano":
                    return "The Pool of Tribute (Extreme)";
                case "Lakshmi":
                    return "Emanation (Extreme)";
                    break;
                case "Shinryu":
                    return "The Minstrel's Ballad: Shinryu's Domain";
                    break;
                case "Byakko":
                    return "The Jade Stoa (Extreme)";
                    break;
                case "Tsukuyomi":
                    return "The Minstrel's Ballad: Tsukuyomi's Pain";
                    break;
                case "Suzaku":
                    return "Hell's Kier (Extreme)";
                case "Bahamut Prime":
                    return "The Unending Coil of Bahamut";
                    break;
                case "The Ultima Weapon":
                    return "Ultimacy";
                    break;
                case "Chaos":
                    return "Alphascape (V1.0)";
                    break;
                case "Midgardsormr":
                    return "Alphascape (V2.0)";
                    break;
                case "Omega":
                    return "Alphascape (V3.0)";
                    break;
                case "Omega-M and Omega-F":
                    return "Alphascape (V4.0)";
                break;
                case "Omega-M and Omega-F (Savage)":
                case "The Final Omega (Savage)":
                    return "Alphascape V4.0 (Savage)";
                case "Chaos (Savage)":
                    return "Alphascape V1.0 (Savage)";
                    break;
                case "Midgardsormr (Savage)":
                    return "Alphascape V2.0 (Savage)";
                    break;
                case "Omega (Savage)":
                    return "Alphascape V3.0 (Savage)";
                    break;
            }

            return "";
        }
    }
}
