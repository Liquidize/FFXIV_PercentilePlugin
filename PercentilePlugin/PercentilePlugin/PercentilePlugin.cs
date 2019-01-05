using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PercentilePlugin.Shared;

namespace PercentilePlugin
{
    public class PercentilePlugin : IActPluginV1
    {
        private Label pluginLabel;
        public PercentileUi percentileUi;
        public static Logger Logger;
        public static PercentileData PercentileData;

        private static readonly int kRequiredNETVersionMajor = 4;
        private static readonly int kRequiredNETVersionMinor = 6;
        private static readonly int kRequiredNETVersionRevision = 1;

        public async void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            Logger = new Logger();
            Logger.Log(LogLevel.Info, "Plugin Init.");


            if (Directory.Exists("PercentilePlugin") != true)
            {
                Logger.Log(LogLevel.Warning, "Creating PercentilePlugin Directory....");
                Directory.CreateDirectory("PercentilePlugin");
            }

            Options.Load();
            Logger.Log(LogLevel.Info, "Loaded Options.");
            PercentileData = PercentileData.Load("PercentilePlugin/parsedata.bin");
            Logger.Log(LogLevel.Info, "Percentile Data Loaded.");

            percentileUi = new PercentileUi();
            pluginLabel = pluginStatusText;
            percentileUi.Dock = DockStyle.Fill;
            pluginScreenSpace.Controls.Add(percentileUi);

            CombatantData.ColumnDefs.Add("Percentile", new CombatantData.ColumnDef("Percentile", true, "FLOAT",
                "Percentile",
                Data => { return GetPercentile(Data).ToString(); },
                Data => { return GetPercentile(Data).ToString(); },
                (Left, Right) => { return GetPercentile(Left).CompareTo(GetPercentile(Right)); }));
            ActGlobals.oFormActMain.ValidateTableSetup();
            CombatantData.ExportVariables.Add("Percentile", new CombatantData.TextExportFormatter("percentile",
                "Percentile", "2 Week Historical Percentile based off current DPS.",
                (Data, Extra) => { return GetPercentile(Data).ToString(); }));
            ActGlobals.oFormActMain.ValidateLists();
            Logger.Log(LogLevel.Info, "Percentile Column Added.");
            pluginStatusText.Text = "Plugin Loaded.";
            percentileUi.lastCheckLbl.Text =
                "Last Updated: " + new DateTime(1970, 1, 1).AddMilliseconds(PercentileData.LastUpdated).ToLocalTime()
                    .ToString("F");
            Logger.Log(LogLevel.Info, "Plugin Loaded");

            var checker = new VersionChecker();
            var local = checker.GetLocalVersion();
            var remote = checker.GetRemoteVersion();
            if (remote.Major == 0 && remote.Minor == 0)
            {
                var result = MessageBox.Show(
                    "Github error while checking PercentilePlugin version. " +
                    "Your current version is " + local + ".\n\n" +
                    "Manually check for newer version now?",
                    "PercentilePlugin Manual Check",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Process.Start(VersionChecker.kReleaseUrl);
            }
            else if (local < remote)
            {
                if (Options.Instance != null && string.IsNullOrEmpty(Options.Instance.RemoteVersionSeen))
                    Options.Instance.RemoteVersionSeen = "0.0.0";

                var remote_seen_before = new Version(Options.Instance.RemoteVersionSeen);
                Options.Instance.RemoteVersionSeen = remote.ToString();

                var update_message = "There is a new version of PercentilePlugin is available at: \n" +
                                     VersionChecker.kReleaseUrl + " \n\n" +
                                     "New version " + remote + " \n" +
                                     "Current version " + local;
                if (remote == remote_seen_before)
                {
                    Logger.Log(LogLevel.Warning, "Remote Version Seen Before.");
                }
                else
                {
                    var result = MessageBox.Show(
                        update_message + "\n\n" +
                        "Get it now?",
                        "PercentilePlugin update available",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        Process.Start(VersionChecker.kReleaseUrl);
                }

                var net_version_str = FileVersionInfo
                    .GetVersionInfo(typeof(int).Assembly.Location).ProductVersion;
                var net_version = net_version_str.Split('.');
                if (int.Parse(net_version[0]) < kRequiredNETVersionMajor ||
                    int.Parse(net_version[1]) < kRequiredNETVersionMinor ||
                    int.Parse(net_version[2]) < kRequiredNETVersionRevision)
                    Logger.Log(LogLevel.Error, "Requires .NET 4.5.1 or above. Using " + net_version_str);


                Options.Instance.RemoteVersionSeen = remote.ToString();
                Options.Instance.Save();
            }

            if (Options.Instance.AutoUpdate)
            {
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (sender, args) => { UpdateData(); };
                backgroundWorker.RunWorkerAsync();
            }
        }

        public async Task UpdateData()
        {
            try
            {
                var request =
                    WebRequest.Create("https://github.com/Liquidize/FFXIV_PercentilePlugin/raw/master/parsedata.bin");
                var response = await request.GetResponseAsync();
                if (response.GetResponseStream() != null)
                    using (var bsonReader = new BsonReader(response.GetResponseStream()))
                    {
                        var serializer = new JsonSerializer();
                        var data = serializer.Deserialize<PercentileData>(bsonReader);
                        if (data != null)
                        {
                            PercentileData = data;
                            var file = new FileStream("PercentilePlugin/parsedata.bin", FileMode.OpenOrCreate);
                            using (var writer = new BsonWriter(file))
                            {
                                serializer.Serialize(writer, data);
                            }

                            file.Close();
                            Logger.Log(LogLevel.Info, "Percentile Data has been updated.");
                            percentileUi.UpdateLabelText();
                        }
                        else
                        {
                            MessageBox.Show("Error in updating data.");
                        }
                    }
            }
            catch (WebException webEx)
            {
                Logger.Log(LogLevel.Error, "Error in updating data.");
                Logger.Log(LogLevel.Error, webEx.ToString());
            }
            catch (JsonException jsonEx)
            {
                Logger.Log(LogLevel.Error, "Error in updating data.");
                Logger.Log(LogLevel.Error, jsonEx.ToString());
            }
        }

        public void DeInitPlugin()
        {
            pluginLabel.Text = "Plugin Exited.";
        }

        public string GetTranslatedStrongest(string boss)
        {
            switch (boss)
            {
                case "カオス":
                    return "Chaos";
                case "ミドガルズオルム":
                    return "Midgardsormr";
                case "オメガ":
                    return "Omega";
                case "朱雀":
                    return "Suzaku";
                case "ツクヨミ":
                    return "Tsukuyomi";
                case "白虎":
                    return "Byakko";
                case "神龍":
                    return "Shinryu";
                case "ラクシュミ":
                    return "Lakshmi";
                case "スサノオ":
                    return "Susano";
                case "背徳の皇帝マティウス":
                    return "Mateus, the Corrupt";
                case "統制者ハシュマリム":
                    return "Hashmal, Bringer of Order";
                case "人馬王ロフォカレ":
                    return "Rofocale";
                case "冷血剣アルガス":
                    return "Argath Thadalfus";
                case "暗黒の雲ファムフリート":
                    return "Famfrit, the Darkening Cloud";
                case "魔人ベリアス":
                    return "Belias, the Gigas";
                case "労働七号":
                    return "Construct 7";
                case "鬼龍ヤズマット":
                    return "Yiazmat";
            }

            return "";
        }

        public string GetBoss(EncounterData enc)
        {
            var strongest = enc.GetStrongestEnemy("YOU");
            var combatant = enc.GetCombatant(strongest);
            strongest = string.IsNullOrEmpty(GetTranslatedStrongest(strongest))
                ? strongest
                : GetTranslatedStrongest(strongest);
            if (enc.ZoneName.ToLower() == "the Weapon's Refrain (Ultimate)".ToLower()) return "The Ultima Weapon";
            if (enc.ZoneName.ToLower() == "the Unending Coil of Bahamut (Ultimate)".ToLower()) return "Bahamut Prime";

            if (enc.ZoneName.Contains("Alphascape (V4.0)")) strongest = "Omega-M and Omega-F";

            if (enc.ZoneName.Contains("Savage")) strongest = strongest + " (Savage)";

            if (enc.ZoneName.Contains("Alphascape") && enc.ZoneName.Contains("4.0") && enc.ZoneName.Contains("Savage"))
            {
                if (combatant.AllOut.ContainsKey("Target Analysis") || combatant.AllOut.ContainsKey("標的識別") ||
                    combatant.AllOut.ContainsKey("Unknown_336C"))
                    return "The Final Omega (Savage)";
                return "Omega-M and Omega-F (Savage)";
            }

            return strongest;
        }

        public double GetPercentile(CombatantData combatant)
        {
            var job = combatant.GetColumnByName("Job").ToUpper();

            if (combatant.Parent == null) return -1;
            if (string.IsNullOrEmpty(job)) return -1;

            var boss = GetBoss(combatant.Parent).ToLower();

            if (!string.IsNullOrEmpty(boss)) return GetRealPercentile(job, boss, combatant.EncDPS);

            return -1;
        }


        public static double GetRealPercentile(string job, string enc, double DPS)
        {
            if (PercentileData.Rankings.ContainsKey(enc) != true) return 0;
            var sequence = PercentileData.Rankings[enc][job].ToArray();
            if (sequence.Length == 0) return 100;
            Array.Sort(sequence);
            var l = 0;
            var r = sequence.Length - 1;
            var index = sequence.Length / 2;

            while (l <= r)
            {
                index = l + (r - l) / 2;

                if (sequence[index] < DPS)
                    l = index + 1;
                else
                    r = index - 1;
            }

            return (100 * index + sequence.Length) / sequence.Length;
        }
    }
}